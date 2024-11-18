using GraphQL;
using Newtonsoft.Json.Linq;

namespace FluentHub.Octokit.Queries.Users
{
	public class NotificationQueries
	{
		public async Task<List<Notification>> GetAllAsync(OctokitV3.NotificationsRequest request = null, OctokitV3.ApiOptions options = null)
		{
			var response = await App.Client.Activity.Notifications.GetAllForCurrent(request, options);

			List<Notification> notifications = new();
			foreach (var item in response)
			{
				Notification indivisual = new()
				{
					Id = Convert.ToInt64(item.Id),
					Unread = item.Unread,
					Url = item.Url,

					Subject = new()
					{
						Title = item.Subject.Title,
					},

					Repository = new()
					{
						Name = item.Repository.Name,
						Owner = new RepositoryOwner()
						{
							AvatarUrl = item.Repository.Owner.AvatarUrl,
							Login = item.Repository.Owner.Login,
						},
					},
				};

				if (item.LastReadAt != null)
				{
					indivisual.LastReadAt = DateTimeOffset.Parse(item.LastReadAt);
					indivisual.LastReadAtHumanized = indivisual.LastReadAt.Humanize();
				}

				if (item.UpdatedAt != null)
				{
					indivisual.UpdatedAt = DateTimeOffset.Parse(item.UpdatedAt);
					indivisual.UpdatedAtHumanized = indivisual.UpdatedAt.Humanize();
				}

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

				var urlItems = item.Subject.Url?.Split('/').ToList();

				switch (item.Subject?.Type)
				{
					case "Issue":
						{
							indivisual.Subject.Type = NotificationSubjectType.Issue;
							indivisual.Subject.Number = Convert.ToInt32(urlItems.LastOrDefault());
							break;
						}
					case "PullRequest":
						{
							indivisual.Subject.Type = NotificationSubjectType.PullRequest;
							indivisual.Subject.Number = Convert.ToInt32(urlItems.LastOrDefault());
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

			var fragments = GetNotificationItemFragments(notifications);
			if (notifications.Count() == 0 || string.IsNullOrEmpty(fragments))
				return notifications;

			// Create a new request with repository info and vaild Issue or PR info
			var request2 = new GraphQLRequest { Query = @$"query {{ {fragments} }}" };

			// Response contains a lot of repository response tokens
			var response2 = await App.GraphQLHttpClient.SendQueryAsync<object>(request2);

			var repositories = ParseGraphQLJsonResponse(response2.Data as JToken, notifications.Count());

			var mappedNotifications = MapRepositoriesToNotifications(notifications, repositories);
			if (mappedNotifications == null)
				return notifications;

			return mappedNotifications;
		}

		private string GetNotificationItemFragments(IReadOnlyList<Notification> notifications)
		{
			string ItemFragments = "";
			int index = 0;

			foreach (var notification in notifications)
			{
				switch (notification.Subject.Type)
				{
					//case NotificationSubjectType.Discussion:
					//case NotificationSubjectType.Commit:
					//case NotificationSubjectType.Release:
					//	break;
					case NotificationSubjectType.Issue:
						{
							var issueFragment = @$"
repo{index}: repository(name: ""{notification.Repository.Name}"", owner: ""{notification.Repository.Owner.Login}"") {{
  Issue: issue(number: {notification.Subject.Number}) {{
	id
	number
	state
	stateReason
  }}
}}
";

							ItemFragments += issueFragment;
							break;
						}
					case NotificationSubjectType.PullRequest:
						{
							var pullRequestFragment = @$"
repo{index}: repository(name: ""{notification.Repository.Name}"", owner: ""{notification.Repository.Owner.Login}"") {{
  PullRequest: pullRequest(number: {notification.Subject.Number}) {{
	id
	number
	isDraft
	state
  }}
}}
";

							ItemFragments += pullRequestFragment;
							break;
						}
				}

				index++;
			}

			return ItemFragments;
		}

		private List<Repository> ParseGraphQLJsonResponse(JToken token, int itemCount)
		{
			List<Repository> repositories = new();

			if (token["errors"] != null)
			{
				var error = token["errors"];
				throw new OctokitGraphQLCore.Deserializers.ResponseDeserializerException(
					(string)error["message"],
					(int)error["locations"][0]["line"],
					(int)error["locations"][0]["column"]);
			}

			for (int index = 0; index < itemCount; index++)
			{
				var repo = token[$"repo{index}"];

				if (repo == null || !repo.HasValues)
				{
					// Add empty
					repositories.Add(new());
					continue;
				}

				var issue = repo["Issue"];
				var pullRequest = repo["PullRequest"];

				// HasValues because issue can be empty
				if (issue != null && issue.HasValues)
				{
					Enum.TryParse(issue["state"].ToString(), true, out IssueState state);
					Enum.TryParse(issue["stateReason"].ToString(), true, out IssueStateReason stateReason);
					var id = new ID(issue["id"].ToString());
					int.TryParse(issue["number"].ToString(), out int number);

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
				else if (pullRequest != null && pullRequest.HasValues)
				{
					Enum.TryParse(pullRequest["state"].ToString(), true, out PullRequestState state);
					var id = new ID(pullRequest["id"].ToString());
					int.TryParse(pullRequest["number"].ToString(), out int number);
					bool.TryParse(pullRequest["isDraft"].ToString(), out bool isDraft);

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

		private List<Notification> MapRepositoriesToNotifications(List<Notification> notifications, IReadOnlyList<Repository> repositories)
		{
			int index = 0;

			if (notifications.Count != repositories.Count)
				return null;

			var zippedData = notifications.Zip(repositories, (notification, repository)
				=> new { Notification = notification, Repository = repository });

			foreach (var item in zippedData)
			{
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

		public async Task<int> GetUnreadCount()
		{
			OctokitV3.NotificationsRequest request = new()
			{
				All = true,
			};

			OctokitV3.ApiOptions options = new()
			{
				PageCount = 1,
				PageSize = 50,
				StartPage = 1
			};

			// Even if there are more than 50 unread items, this method will only count up to a maximum of 50.
			var response = await App.Client.Activity.Notifications.GetAllForCurrent(request, options);

			int unreadCount = 0;
			foreach (var indivisual in response)
			{
				if (indivisual.Unread) unreadCount++;
			}

			return unreadCount;
		}
	}
}
