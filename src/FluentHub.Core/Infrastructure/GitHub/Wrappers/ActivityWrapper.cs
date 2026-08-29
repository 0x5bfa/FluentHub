// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Infrastructure.GitHub.Wrappers
{
	internal sealed class ActivityWrapper
	{
		public List<Activity> Wrap(IReadOnlyList<OctokitRest.GitHubActivityEvent> response)
		{
			var activities = new List<Activity>();

			foreach (var item in response)
			{
				if (item.Actor is not { } actor)
					continue;

				var repoNameParts = item.Repo?.Name.Split('/');
				var repository = new Repository
				{
					Name = repoNameParts?.ElementAtOrDefault(1) ?? string.Empty,
					Owner = new RepositoryOwner
					{
						Login = repoNameParts?.ElementAtOrDefault(0) ?? string.Empty,
					},
				};
				var user = new User
				{
					AvatarUrl = actor.AvatarUrl ?? string.Empty,
					Login = actor.Login,
					Name = actor.Name ?? string.Empty,
				};
				var organization = new Organization
				{
					AvatarUrl = item.Org?.AvatarUrl ?? string.Empty,
					Login = item.Org?.Login ?? string.Empty,
				};
				var activity = new Activity
				{
					CreatedAt = item.CreatedAt,
					CreatedAtHumanized = item.CreatedAt.ToRelativeTime(),
					Id = item.Id,
					Public = item.Public,
					Repository = repository,
					Actor = user,
					Organization = organization,
				};
				var payload = item.Payload;

				switch (item.Type)
				{
					case "CheckRunEvent":
						activity.Type = ActivityKind.CheckRunEvent;
						break;
					case "CheckSuiteEvent":
						activity.Type = ActivityKind.CheckSuiteEvent;
						break;
					case "CommitCommentEvent":
					case "CommitComment":
						activity.Type = ActivityKind.CommitComment;
						break;
					case "CreateEvent" when payload is not null:
						activity.Type = ActivityKind.CreateEvent;
						activity.Details.CreateEvent = new()
						{
							Description = payload.Description,
							MasterBranch = payload.MasterBranch,
							Ref = payload.Ref,
						};
						break;
					case "DeleteEvent" when payload is not null:
						activity.Type = ActivityKind.DeleteEvent;
						activity.Details.DeleteEvent = new() { Ref = payload.Ref };
						break;
					case "ForkEvent" when payload?.Forkee?.Owner is { } forkOwner:
						activity.Type = ActivityKind.ForkEvent;
						activity.Details.ForkEvent = new()
						{
							Forkee = new()
							{
								Name = payload.Forkee.Name,
								Owner = new RepositoryOwner
								{
									AvatarUrl = forkOwner.AvatarUrl ?? string.Empty,
									Login = forkOwner.Login,
								},
							},
						};
						break;
					case "IssueCommentEvent" when payload?.Comment is { } issueComment && payload.Issue is { } commentedIssue:
						activity.Type = ActivityKind.IssueCommentEvent;
						activity.Details.IssueCommentEvent = new()
						{
							Action = payload.Action,
							Comment = new() { Body = issueComment.Body ?? string.Empty },
							Issue = new() { Number = commentedIssue.Number },
						};
						break;
					case "IssuesEvent":
					case "IssueEvent":
						if (payload?.Issue is not { } issue)
							continue;

						activity.Type = ActivityKind.IssueEvent;
						activity.Details.IssueEvent = new()
						{
							Action = payload.Action,
							Issue = new()
							{
								Closed = issue.ClosedAt is not null,
								Number = issue.Number,
								Title = issue.Title ?? string.Empty,
								UpdatedAt = issue.UpdatedAt.GetValueOrDefault(),
								UpdatedAtHumanized = issue.UpdatedAt.ToRelativeTime(),
								Repository = repository,
							},
						};
						break;
					case "PullRequestReviewCommentEvent":
					case "PullRequestComment":
						if (payload?.PullRequest is not { } commentedPullRequest)
							continue;

						activity.Type = ActivityKind.PullRequestComment;
						activity.Details.PullRequestCommentEvent = new()
						{
							Action = payload.Action,
							PullRequest = new() { Number = commentedPullRequest.Number },
						};
						break;
					case "PullRequestEvent" when payload?.PullRequest is { } pullRequest:
						activity.Type = ActivityKind.PullRequestEvent;
						activity.Details.PullRequestEvent = new()
						{
							Action = payload.Action,
							PullRequest = new()
							{
								Closed = pullRequest.ClosedAt is not null,
								Number = pullRequest.Number,
								Title = pullRequest.Title ?? string.Empty,
								UpdatedAt = pullRequest.UpdatedAt,
								UpdatedAtHumanized = pullRequest.UpdatedAt.ToRelativeTime(),
								IsDraft = pullRequest.Draft,
								Merged = pullRequest.Merged,
								Repository = repository,
							},
						};
						break;
					case "PullRequestReviewEvent":
						activity.Type = ActivityKind.PullRequestReviewEvent;
						break;
					case "PushEvent" when payload is not null:
						activity.Type = ActivityKind.PushEvent;
						activity.Details.PushEvent = new()
						{
							Commits = payload.Commits?
								.Select(commit => new ActivityCommit
								{
									Message = commit.Message ?? string.Empty,
									Sha = commit.Sha ?? string.Empty,
									User = new()
									{
										Login = commit.Author?.Name ?? string.Empty,
										Name = commit.Author?.Name ?? string.Empty,
									},
								})
								.ToList() ?? [],
							Head = payload.Head,
							Ref = payload.Ref,
							Size = payload.Size,
						};
						break;
					case "ReleaseEvent" when payload?.Release is { } release:
						activity.Type = ActivityKind.ReleaseEvent;
						activity.Details.ReleaseEvent = new()
						{
							Action = payload.Action,
							Release = new()
							{
								Name = release.Name,
								Description = release.Body,
							},
							Sender = user,
						};
						break;
					case "WatchEvent" when payload is not null:
						activity.Type = ActivityKind.WatchEvent;
						activity.Details.StarredEvent = new() { Action = payload.Action };
						break;
					case "StatusEvent":
						activity.Type = ActivityKind.StatusEvent;
						break;
					default:
						continue;
				}

				activities.Add(activity);
			}

			return activities;
		}
	}
}
