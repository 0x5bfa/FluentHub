using System.Text.Json;

using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public class NotificationQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public NotificationQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<Notification>> GetAllAsync(
			OctokitRest.NotificationRequest? request = null,
			OctokitRest.PageOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunRestAsync(
				(client, token) => client.Notifications.GetAllAsync(request, options, token),
				cancellationToken);

			List<Notification> notifications = new();
			foreach (var item in response)
			{
				if (item.Subject is not { } subject ||
					item.Repository?.Owner is not { } owner ||
					!long.TryParse(item.Id, out var id))
					continue;

				Notification indivisual = new()
				{
					Id = id,
					Unread = item.Unread,
					Url = item.Url,

					Subject = new()
					{
						Title = subject.Title,
					},

					Repository = new()
					{
						Name = item.Repository.Name,
						Owner = new RepositoryOwner()
						{
							AvatarUrl = owner.AvatarUrl ?? string.Empty,
							Login = owner.Login,
						},
					},
				};

				if (item.LastReadAt is { } lastReadAt)
				{
					indivisual.LastReadAt = lastReadAt;
					indivisual.LastReadAtHumanized = indivisual.LastReadAt.ToRelativeTime();
				}

				indivisual.UpdatedAt = item.UpdatedAt;
				indivisual.UpdatedAtHumanized = indivisual.UpdatedAt.ToRelativeTime();

				indivisual.Reason = item.Reason switch
				{
					"assign" => "You were assigned to the issue.",
					"author" => "You created the thread.",
					"comment" => "You commented on the thread.",
					"ci_activity" => "A workflow that you triggered was successful.",
					"invitation" => "You accepted an invitation to contribute to the repository.",
					"manual" => "You subscribed to the thread.",
					"mention" => "You were mentioned.",
					"review_requested" => "You or a team you are a member of was requested to review a pull request.",
					"security_alert" => "A vulnerability was detected in your repository.",
					"state_change" => "You changed the state of the thread.",
					"subscribed" => "You started watching the repository.",
					"team_mention" => "You are on a team that was mentioned.",
					_ => "",
				};

				var itemNumber = subject.Url?.Split('/').LastOrDefault();

				switch (subject.Type)
				{
					case "Issue":
						{
							indivisual.Subject.Type = NotificationSubjectType.Issue;
							indivisual.Subject.Number = Convert.ToInt32(itemNumber);
							break;
						}
					case "PullRequest":
						{
							indivisual.Subject.Type = NotificationSubjectType.PullRequest;
							indivisual.Subject.Number = Convert.ToInt32(itemNumber);
							break;
						}
					case "Discussion":
						{
							indivisual.Subject.Type = NotificationSubjectType.Discussion;
							break;
						}
					case "Commit":
						{
							indivisual.Subject.Type = NotificationSubjectType.Commit;
							break;
						}
				}

				notifications.Add(indivisual);
			}

			// NOTE:
			// The first Octokit v3 response has insufficient content, so gather the necessary info
			// from the response to get the necessary data and create a new Octokit v4 request

			var notificationQuery = BuildNotificationQuery(notifications);
			if (notificationQuery is null)
				return notifications;

			var response2 = await _gitHub.RunGraphQLAsync(
				notificationQuery,
				GitHubGraphQLJsonContext.Default.JsonElement,
				writer => WriteNotificationVariables(writer, notifications),
				cancellationToken);

			var repositories = ParseGraphQLJsonResponse(response2, notifications.Count);

			var mappedNotifications = MapRepositoriesToNotifications(notifications, repositories);
			if (mappedNotifications == null)
				return notifications;

			return mappedNotifications;
		}

		private static string? BuildNotificationQuery(IReadOnlyList<Notification> notifications)
		{
			var definitions = new List<string>();
			var selections = new StringBuilder();

			for (var index = 0; index < notifications.Count; index++)
			{
				var notification = notifications[index];
				if (notification.Subject is null || notification.Repository?.Owner is null)
					continue;

				switch (notification.Subject.Type)
				{
					//case NotificationSubjectType.Discussion:
					//case NotificationSubjectType.Commit:
					//case NotificationSubjectType.Release:
					//	break;
					case NotificationSubjectType.Issue:
						{
							definitions.Add($"$name{index}: String!");
							definitions.Add($"$owner{index}: String!");
							definitions.Add($"$number{index}: Int!");
							selections.Append($$"""
repo{{index}}: repository(name: $name{{index}}, owner: $owner{{index}}) {
  Issue: issue(number: $number{{index}}) {
	id
	number
	state
	stateReason
  }
}
""");
							break;
						}
					case NotificationSubjectType.PullRequest:
						{
							definitions.Add($"$name{index}: String!");
							definitions.Add($"$owner{index}: String!");
							definitions.Add($"$number{index}: Int!");
							selections.Append($$"""
repo{{index}}: repository(name: $name{{index}}, owner: $owner{{index}}) {
  PullRequest: pullRequest(number: $number{{index}}) {
	id
	number
	isDraft
	state
  }
}
""");
							break;
						}
				}
			}

			return definitions.Count == 0
				? null
				: $"query({string.Join(", ", definitions)}) {{\n{selections}}}";
		}

		private static void WriteNotificationVariables(
			Utf8JsonWriter writer,
			IReadOnlyList<Notification> notifications)
		{
			for (var index = 0; index < notifications.Count; index++)
			{
				var notification = notifications[index];
				if (notification.Subject?.Type is not (NotificationSubjectType.Issue or NotificationSubjectType.PullRequest) ||
					notification.Repository?.Owner is null)
				{
					continue;
				}

				writer.WriteString($"name{index}", notification.Repository.Name);
				writer.WriteString($"owner{index}", notification.Repository.Owner.Login);
				writer.WriteNumber($"number{index}", notification.Subject.Number);
			}
		}

		private static List<Repository> ParseGraphQLJsonResponse(JsonElement token, int itemCount)
		{
			List<Repository> repositories = new();

			if (token.ValueKind != JsonValueKind.Object)
				return repositories;

			for (int index = 0; index < itemCount; index++)
			{
				if (!token.TryGetProperty($"repo{index}", out var repo) ||
					repo.ValueKind != JsonValueKind.Object)
				{
					// Add empty
					repositories.Add(new());
					continue;
				}

				if (repo.TryGetProperty("Issue", out var issue) && issue.ValueKind == JsonValueKind.Object)
				{
					Enum.TryParse(issue.GetProperty("state").GetString(), true, out IssueState state);
					Enum.TryParse(issue.GetProperty("stateReason").GetString(), true, out IssueStateReason stateReason);
					var id = new ID(issue.GetProperty("id").GetString() ?? string.Empty);
					var number = issue.GetProperty("number").GetInt32();

					repositories.Add(new()
					{
						Issue = new()
						{
							Id = id,
							Number = number,
							State = state,
							StateReason = stateReason,
						},
					});
				}
				else if (repo.TryGetProperty("PullRequest", out var pullRequest) &&
					pullRequest.ValueKind == JsonValueKind.Object)
				{
					Enum.TryParse(pullRequest.GetProperty("state").GetString(), true, out PullRequestState state);
					var id = new ID(pullRequest.GetProperty("id").GetString() ?? string.Empty);
					var number = pullRequest.GetProperty("number").GetInt32();
					var isDraft = pullRequest.GetProperty("isDraft").GetBoolean();

					repositories.Add(new()
					{
						PullRequest = new()
						{
							Id = id,
							Number = number,
							IsDraft = isDraft,
							State = state,
						},
					});
				}
				else
				{
					// Add empty
					repositories.Add(new());
					continue;
				}
			}

			return repositories;
		}

		private List<Notification>? MapRepositoriesToNotifications(List<Notification> notifications, IReadOnlyList<Repository> repositories)
		{
			int index = 0;

			if (notifications.Count != repositories.Count)
				return null;

			var zippedData = notifications.Zip(repositories, (notification, repository)
				=> new { Notification = notification, Repository = repository });

			foreach (var item in zippedData)
			{
				if (item.Notification.Subject is null)
					continue;

				switch (item.Notification.Subject.Type)
				{
					//case NotificationSubjectType.Discussion:
					//case NotificationSubjectType.Commit:
					//case NotificationSubjectType.Release:
					//	break;
					case NotificationSubjectType.Issue:
						{
							if (item.Repository.Issue != null)
							{
								item.Notification.Subject.Number = item.Repository.Issue.Number;

								switch (item.Repository.Issue.State)
								{
									case IssueState.Open:
									{
										item.Notification.Subject.Type = NotificationSubjectType.IssueOpen;
										break;
									}
									case IssueState.Closed:
									{
										switch (item.Repository.Issue.StateReason)
										{
											case IssueStateReason.Completed:
												item.Notification.Subject.Type = NotificationSubjectType.IssueClosedAsCompleted;
												break;
											case IssueStateReason.Reopened:
											case IssueStateReason.NotPlanned:
												item.Notification.Subject.Type = NotificationSubjectType.IssueClosedAsNotPlanned;
												break;
										}
										break;
									}
								}
							}
							
							break;
						}
					case NotificationSubjectType.PullRequest:
						{
							if (item.Repository.PullRequest != null)
							{
								item.Notification.Subject.Number = item.Repository.PullRequest.Number;

								switch (item.Repository.PullRequest.State)
								{
									case PullRequestState.Open:
										item.Notification.Subject.Type = item.Repository.PullRequest.IsDraft ?
											NotificationSubjectType.PullRequestDraft :
											NotificationSubjectType.PullRequestOpen;
										break;
									case PullRequestState.Closed:
										item.Notification.Subject.Type = NotificationSubjectType.PullRequestClosed;
										break;
									case PullRequestState.Merged:
										item.Notification.Subject.Type = NotificationSubjectType.PullRequestMerged;
										break;
								}
							}

							break;
						}
				}

				item.Notification.Subject.TypeHumanized = item.Notification.Subject.Type.ToString();
				index++;
			}

			return notifications;
		}

		public async Task<int> GetUnreadCountAsync(CancellationToken cancellationToken = default)
		{
			OctokitRest.NotificationRequest request = new()
			{
				All = true,
			};

			OctokitRest.PageOptions options = new()
			{
				PageCount = 1,
				PageSize = 50,
				StartPage = 1
			};

			// Even if there are more than 50 unread items, this method will only count up to a maximum of 50.
			var response = await _gitHub.RunRestAsync(
				(client, token) => client.Notifications.GetAllAsync(request, options, token),
				cancellationToken);

			int unreadCount = 0;
			foreach (var indivisual in response)
			{
				if (indivisual.Unread) unreadCount++;
			}

			return unreadCount;
		}
	}
}
