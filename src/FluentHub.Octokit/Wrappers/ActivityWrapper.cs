using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHub.Octokit.Wrappers
{
    internal class ActivityWrapper
    {
        public List<Activity> Wrap(IReadOnlyList<OctokitOriginal.Activity> response)
        {
            List<Activity> activities = new();

            foreach (var item in response)
            {
                Activity indivisual = new()
                {
                    CreatedAt = item.CreatedAt,

                    Repository = new()
                    {
                        Name = item.Repo?.Name.Split('/')[1],
                        Owner = new RepositoryOwner()
                        {
                            AvatarUrl = item.Repo?.Owner.AvatarUrl,
                            Login = item.Repo?.Owner.Login.Split('/')[0],
                        },
                    },

                    Actor = new()
                    {
                        AvatarUrl = item.Actor.AvatarUrl,
                        Login = item.Actor.Login,
                        Name = item.Actor.Name,
                    },

                    Organization = new()
                    {
                        AvatarUrl = item.Org?.AvatarUrl,
                        Login = item.Org?.Login,
                        Name = item.Org?.Name,
                    },
                };

                switch (item.Type)
                {
                    case "CheckRunEvent":
                        indivisual.Type = ActivityPayloadType.CheckRunEvent;
                        break;
                    case "CheckSuiteEvent":
                        indivisual.Type = ActivityPayloadType.CheckSuiteEvent;
                        break;
                    case "CommitComment":
                        indivisual.Type = ActivityPayloadType.CommitComment;
                        break;
                    case "CreateEvent":
                        indivisual.Type = ActivityPayloadType.CreateEvent;
                        var createEventPayload = (OctokitOriginal.CreateEventPayload)item.Payload;
                        indivisual.PayloadSets.CreateEventPayload = new()
                        {
                            Description = createEventPayload.Description,
                            MasterBranch = createEventPayload.MasterBranch,
                            Ref = createEventPayload.Ref,
                        };
                        break;
                    case "DeleteEvent":
                        indivisual.Type = ActivityPayloadType.DeleteEvent;
                        var deleteEventPayload = (OctokitOriginal.DeleteEventPayload)item.Payload;
                        indivisual.PayloadSets.DeleteEventPayload = new()
                        {
                            Ref = deleteEventPayload.Ref,
                        };
                        break;
                    case "ForkEvent":
                        indivisual.Type = ActivityPayloadType.ForkEvent;
                        var forkEventPayload = (OctokitOriginal.ForkEventPayload)item.Payload;
                        indivisual.PayloadSets.ForkEventPayload = new()
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
                        break;
                    case "IssueCommentEvent":
                        indivisual.Type = ActivityPayloadType.IssueCommentEvent;
                        var issueCommentPayload = (OctokitOriginal.IssueCommentPayload)item.Payload;
                        indivisual.PayloadSets.IssueCommentPayload = new()
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
                        break;
                    case "IssueEvent":
                        indivisual.Type = ActivityPayloadType.IssueEvent;
                        break;
                    case "PullRequestComment":
                        indivisual.Type = ActivityPayloadType.PullRequestComment;
                        break;
                    case "PullRequestEvent":
                        indivisual.Type = ActivityPayloadType.PullRequestEvent;
                        break;
                    case "PullRequestReviewEvent":
                        indivisual.Type = ActivityPayloadType.PullRequestReviewEvent;
                        break;
                    case "PushEvent":
                        indivisual.Type = ActivityPayloadType.PushEvent;
                        var pushEventPayload = (OctokitOriginal.PushEventPayload)item.Payload;
                        indivisual.PayloadSets.PushEventPayload = new()
                        {
                            Head = pushEventPayload.Head,
                            Ref = pushEventPayload.Ref,
                            Size = pushEventPayload.Size,
                        };
                        break;
                    case "PushWebhookCommit":
                        indivisual.Type = ActivityPayloadType.PushWebhookCommit;
                        break;
                    case "PushWebhookCommitter":
                        indivisual.Type = ActivityPayloadType.PushWebhookCommitter;
                        break;
                    case "PushWebhook":
                        indivisual.Type = ActivityPayloadType.PushWebhook;
                        break;
                    case "ReleaseEvent":
                        indivisual.Type = ActivityPayloadType.ReleaseEvent;
                        var releaseEventPayload = (OctokitOriginal.ReleaseEventPayload)item.Payload;
                        indivisual.PayloadSets.ReleaseEventPayload = new()
                        {
                            Action = releaseEventPayload.Action,
                            Release = new()
                            {
                                Name = releaseEventPayload.Release.Name,
                            }
                        };
                        break;
                    case "WatchEvent":
                        indivisual.Type = ActivityPayloadType.WatchEvent;
                        var watchEventPayload = (OctokitOriginal.StarredEventPayload)item.Payload;
                        indivisual.PayloadSets.StarredEventPayload = new()
                        {
                            Action = watchEventPayload.Action,
                        };
                        break;
                    case "StatusEvent":
                        indivisual.Type = ActivityPayloadType.StatusEvent;
                        break;
                }

                activities.Add(indivisual);
            }

            return activities;
        }
    }
}
