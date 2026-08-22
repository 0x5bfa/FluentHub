// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FluentHub.Core.Wrappers
{
	internal class ActivityWrapper
	{
		public List<Activity> Wrap(IReadOnlyList<OctokitV3.Activity> response)
		{
			List<Activity> activities = new();

			foreach (var item in response)
			{
				var repoNameParts = item.Repo?.Name.Split('/');
				Repository itemRep = new()
				{
					Name = repoNameParts?.ElementAtOrDefault(1) ?? string.Empty,
					Owner = new RepositoryOwner()
					{
						AvatarUrl = item.Repo?.Owner?.AvatarUrl ?? string.Empty,
						Login = repoNameParts?.ElementAtOrDefault(0) ?? string.Empty,
					}
				};

				User itemUser = new()
				{
					AvatarUrl = item.Actor.AvatarUrl,
					Login = item.Actor.Login,
					Name = item.Actor.Name,
				};

				Organization itemOrganization = new()
				{
					AvatarUrl = item.Org?.AvatarUrl ?? string.Empty,
					Login = item.Org?.Login ?? string.Empty,
					Name = item.Org?.Name ?? string.Empty
				};

				Activity indivisual = new()
				{
					CreatedAt = item.CreatedAt,

					CreatedAtHumanized = item.CreatedAt.ToRelativeTime(),

					Id = item.Id,

					Public = item.Public,

					Repository = itemRep,

					Actor = itemUser,

					Organization = itemOrganization,
				};

				switch (item.Type)
				{
					case "CheckRunEvent":
						indivisual.Type = ActivityKind.CheckRunEvent;
						break;
					case "CheckSuiteEvent":
						indivisual.Type = ActivityKind.CheckSuiteEvent;
						break;
					case "CommitComment":
						indivisual.Type = ActivityKind.CommitComment;
						break;
					case "CreateEvent":
						{
							indivisual.Type = ActivityKind.CreateEvent;
							var createEventPayload = (OctokitV3.CreateEventPayload)item.Payload;
							indivisual.Details.CreateEvent = new()
							{
								Description = createEventPayload.Description,
								MasterBranch = createEventPayload.MasterBranch,
								Ref = createEventPayload.Ref,
							};
						}
						break;
					case "DeleteEvent":
						{
							indivisual.Type = ActivityKind.DeleteEvent;
							var deleteEventPayload = (OctokitV3.DeleteEventPayload)item.Payload;
							indivisual.Details.DeleteEvent = new()
							{
								Ref = deleteEventPayload.Ref,
							};
						}
						break;
					case "ForkEvent":
						{
							indivisual.Type = ActivityKind.ForkEvent;
							var forkEventPayload = (OctokitV3.ForkEventPayload)item.Payload;
							indivisual.Details.ForkEvent = new()
							{
								Forkee = new()
								{
									Name = forkEventPayload.Forkee.Name,
									Owner = new RepositoryOwner()
									{
										AvatarUrl = forkEventPayload.Forkee.Owner.AvatarUrl,
										Login = forkEventPayload.Forkee.Owner.Login,
									},
								},
							};
						}
						break;
					case "IssueCommentEvent":
						{
							indivisual.Type = ActivityKind.IssueCommentEvent;
							var issueCommentPayload = (OctokitV3.IssueCommentPayload)item.Payload;
							indivisual.Details.IssueCommentEvent = new()
							{
								Action = issueCommentPayload.Action,
								Comment = new()
								{
									Body = issueCommentPayload.Comment.Body,
								},
								Issue = new()
								{
									Number = issueCommentPayload.Issue.Number,
								}
							};
						}
						break;
					case "IssueEvent":
						{
							indivisual.Type = ActivityKind.IssueEvent;
							var issueEventPayload = (OctokitV3.IssueEventPayload)item.Payload;
							indivisual.Details.IssueEvent = new()
							{
								Action = issueEventPayload.Action,
								Issue = new()
								{
									Closed = issueEventPayload.Issue.ClosedAt is not null,
									Number = issueEventPayload.Issue.Number,
									Title = issueEventPayload.Issue.Title,
									UpdatedAt = issueEventPayload.Issue.UpdatedAt.GetValueOrDefault(),
									UpdatedAtHumanized = issueEventPayload.Issue.UpdatedAt.ToRelativeTime(),

									Repository = itemRep,
								},
							};
						}
						break;
					case "PullRequestComment":
						{
							indivisual.Type = ActivityKind.PullRequestComment;
							var pullRequestCommentPayload = (OctokitV3.PullRequestCommentPayload)item.Payload;
							indivisual.Details.PullRequestCommentEvent = new()
							{
								Action = pullRequestCommentPayload.Action,
								PullRequest = new()
								{
									Number = pullRequestCommentPayload.PullRequest.Number,
								},
							};
						}
						break;
					case "PullRequestEvent":
						{
							indivisual.Type = ActivityKind.PullRequestEvent;
							var pullRequestPayload = (OctokitV3.PullRequestEventPayload)item.Payload;
							indivisual.Details.PullRequestEvent = new()
							{
								Action = pullRequestPayload.Action,
								PullRequest = new()
								{
									Closed = pullRequestPayload.PullRequest.ClosedAt is not null,
									Number = pullRequestPayload.PullRequest.Number,
									Title = pullRequestPayload.PullRequest.Title,
									UpdatedAt = pullRequestPayload.PullRequest.UpdatedAt,
									UpdatedAtHumanized = pullRequestPayload.PullRequest.UpdatedAt.ToRelativeTime(),
									IsDraft = pullRequestPayload.PullRequest.Draft,
									Merged = pullRequestPayload.PullRequest.Merged,

									Repository = itemRep,
								},
							};
						}
						break;
					case "PullRequestReviewEvent":
						{
							indivisual.Type = ActivityKind.PullRequestReviewEvent;
						}
						break;
					case "PushEvent":
						{
							indivisual.Type = ActivityKind.PushEvent;
							var pushEventPayload = (OctokitV3.PushEventPayload)item.Payload;
							indivisual.Details.PushEvent = new()
							{
								Commits = pushEventPayload.Commits?
									.Select(commit => new ActivityCommit
									{
										Message = commit.Message,
										Sha = commit.Sha,
										User = new()
										{
											AvatarUrl = commit.User?.AvatarUrl ?? string.Empty,
											Login = commit.User?.Login ?? commit.Author?.Name ?? string.Empty,
											Name = commit.User?.Name ?? commit.Author?.Name ?? string.Empty,
										},
									})
									.ToList() ?? [],
								Head = pushEventPayload.Head,
								Ref = pushEventPayload.Ref,
								Size = pushEventPayload.Size,
							};
						}
						break;
					case "ReleaseEvent":
						{
							indivisual.Type = ActivityKind.ReleaseEvent;
							var releaseEventPayload = (OctokitV3.ReleaseEventPayload)item.Payload;
							indivisual.Details.ReleaseEvent = new()
							{
								Action = releaseEventPayload.Action,
								Release = new()
								{
									Name = releaseEventPayload.Release.Name,
									Description = releaseEventPayload.Release.Body,
								},
								Sender = itemUser,
							};
						}
						break;
					case "WatchEvent":
						{
							indivisual.Type = ActivityKind.WatchEvent;
							var watchEventPayload = (OctokitV3.StarredEventPayload)item.Payload;
							indivisual.Details.StarredEvent = new()
							{
								Action = watchEventPayload.Action,
							};
						}
						break;
					case "StatusEvent":
						{
							indivisual.Type = ActivityKind.StatusEvent;
						}
						break;
				}

				activities.Add(indivisual);
			}

			return activities;
		}
	}
}
