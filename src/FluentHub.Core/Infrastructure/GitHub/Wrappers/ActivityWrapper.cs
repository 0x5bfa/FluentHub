// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FluentHub.Core.Infrastructure.GitHub.Wrappers
{
	internal class ActivityWrapper
	{
		public List<Activity> Wrap(IReadOnlyList<OctokitV3.Activity> response)
		{
			List<Activity> activities = new();

			foreach (var item in response)
			{
				if (item?.Actor is not { } actor)
					continue;

				var repoNameParts = item.Repo?.Name?.Split('/');
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
					AvatarUrl = actor.AvatarUrl ?? string.Empty,
					Login = actor.Login ?? string.Empty,
					Name = actor.Name ?? string.Empty,
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
							if (item.Payload is not OctokitV3.CreateEventPayload createEventPayload)
								continue;

							indivisual.Type = ActivityKind.CreateEvent;
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
							if (item.Payload is not OctokitV3.DeleteEventPayload deleteEventPayload)
								continue;

							indivisual.Type = ActivityKind.DeleteEvent;
							indivisual.Details.DeleteEvent = new()
							{
								Ref = deleteEventPayload.Ref,
							};
						}
						break;
					case "ForkEvent":
						{
							if (item.Payload is not OctokitV3.ForkEventPayload forkEventPayload ||
								forkEventPayload.Forkee?.Owner is not { } forkeeOwner)
								continue;

							indivisual.Type = ActivityKind.ForkEvent;
							indivisual.Details.ForkEvent = new()
							{
								Forkee = new()
								{
									Name = forkEventPayload.Forkee.Name ?? string.Empty,
									Owner = new RepositoryOwner()
									{
										AvatarUrl = forkeeOwner.AvatarUrl ?? string.Empty,
										Login = forkeeOwner.Login ?? string.Empty,
									},
								},
							};
						}
						break;
					case "IssueCommentEvent":
						{
							if (item.Payload is not OctokitV3.IssueCommentPayload issueCommentPayload ||
								issueCommentPayload.Comment is not { } issueComment ||
								issueCommentPayload.Issue is not { } commentedIssue)
								continue;

							indivisual.Type = ActivityKind.IssueCommentEvent;
							indivisual.Details.IssueCommentEvent = new()
							{
								Action = issueCommentPayload.Action,
								Comment = new()
								{
									Body = issueComment.Body,
								},
								Issue = new()
								{
									Number = commentedIssue.Number,
								}
							};
						}
						break;
					case "IssueEvent":
						{
							if (item.Payload is not OctokitV3.IssueEventPayload issueEventPayload ||
								issueEventPayload.Issue is not { } issue)
								continue;

							indivisual.Type = ActivityKind.IssueEvent;
							indivisual.Details.IssueEvent = new()
							{
								Action = issueEventPayload.Action,
								Issue = new()
								{
									Closed = issue.ClosedAt is not null,
									Number = issue.Number,
									Title = issue.Title,
									UpdatedAt = issue.UpdatedAt.GetValueOrDefault(),
									UpdatedAtHumanized = issue.UpdatedAt.ToRelativeTime(),

									Repository = itemRep,
								},
							};
						}
						break;
					case "PullRequestComment":
						{
							if (item.Payload is not OctokitV3.PullRequestCommentPayload pullRequestCommentPayload ||
								pullRequestCommentPayload.PullRequest is not { } commentedPullRequest)
								continue;

							indivisual.Type = ActivityKind.PullRequestComment;
							indivisual.Details.PullRequestCommentEvent = new()
							{
								Action = pullRequestCommentPayload.Action,
								PullRequest = new()
								{
									Number = commentedPullRequest.Number,
								},
							};
						}
						break;
					case "PullRequestEvent":
						{
							if (item.Payload is not OctokitV3.PullRequestEventPayload pullRequestPayload ||
								pullRequestPayload.PullRequest is not { } pullRequest)
								continue;

							indivisual.Type = ActivityKind.PullRequestEvent;
							indivisual.Details.PullRequestEvent = new()
							{
								Action = pullRequestPayload.Action,
								PullRequest = new()
								{
									Closed = pullRequest.ClosedAt is not null,
									Number = pullRequest.Number,
									Title = pullRequest.Title,
									UpdatedAt = pullRequest.UpdatedAt,
									UpdatedAtHumanized = pullRequest.UpdatedAt.ToRelativeTime(),
									IsDraft = pullRequest.Draft,
									Merged = pullRequest.Merged,

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
							if (item.Payload is not OctokitV3.PushEventPayload pushEventPayload)
								continue;

							indivisual.Type = ActivityKind.PushEvent;
							indivisual.Details.PushEvent = new()
							{
								Commits = pushEventPayload.Commits?
									.Where(commit => commit is not null)
									.Select(commit => new ActivityCommit
									{
										Message = commit.Message ?? string.Empty,
										Sha = commit.Sha ?? string.Empty,
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
							if (item.Payload is not OctokitV3.ReleaseEventPayload releaseEventPayload ||
								releaseEventPayload.Release is not { } release)
								continue;

							indivisual.Type = ActivityKind.ReleaseEvent;
							indivisual.Details.ReleaseEvent = new()
							{
								Action = releaseEventPayload.Action,
								Release = new()
								{
									Name = release.Name,
									Description = release.Body,
								},
								Sender = itemUser,
							};
						}
						break;
					case "WatchEvent":
						{
							if (item.Payload is not OctokitV3.StarredEventPayload watchEventPayload)
								continue;

							indivisual.Type = ActivityKind.WatchEvent;
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
