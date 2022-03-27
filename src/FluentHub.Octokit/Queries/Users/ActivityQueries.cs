using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Users
{
    public class ActivityQueries
    {
        public ActivityQueries() => new App();

        public async Task<List<Models.Activity>> GetAllAsync(string login)
        {
            global::Octokit.ApiOptions options = new() { PageCount = 1, PageSize = 30, StartPage = 1 };
            var events = await App.Client.Activity.Events.GetAllUserReceived(login, options);

            List<Models.Activity> activities = new();

            foreach (var eventItem in events)
            {
                Models.Activity activityItem = new();

                switch (eventItem.Type)
                {
                    case "CheckRunEvent":
                        activityItem.CheckRunEventPayload = eventItem.Payload as global::Octokit.CheckRunEventPayload;
                        activityItem.IsCheckRunEvent = true;
                        break;
                    case "CheckSuiteEvent":
                        activityItem.CheckSuiteEventPayload = eventItem.Payload as global::Octokit.CheckSuiteEventPayload;
                        activityItem.IsCheckSuiteEvent = true;
                        break;
                    case "CommitCommentEvent":
                        activityItem.CommitCommentPayload = eventItem.Payload as global::Octokit.CommitCommentPayload;
                        activityItem.IsCommitComment = true;
                        break;
                    case "CreateEvent":
                        activityItem.CreateEventPayload = eventItem.Payload as global::Octokit.CreateEventPayload;
                        activityItem.IsCreateEvent = true;
                        break;
                    case "DeleteEvent":
                        activityItem.DeleteEventPayload = eventItem.Payload as global::Octokit.DeleteEventPayload;
                        activityItem.IsDeleteEvent = true;
                        break;
                    case "ForkEvent":
                        activityItem.ForkEventPayload = eventItem.Payload as global::Octokit.ForkEventPayload;
                        activityItem.IsForkEvent = true;
                        break;
                    case "IssueCommentEvent":
                        activityItem.IssueCommentPayload = eventItem.Payload as global::Octokit.IssueCommentPayload;
                        activityItem.IsIssueComment = true;
                        break;
                    case "IssuesEvent":
                        activityItem.IssueEventPayload = eventItem.Payload as global::Octokit.IssueEventPayload;
                        activityItem.IsIssueEvent = true;
                        break;
                    case "PullRequestEvent":
                        activityItem.PullRequestEventPayload = eventItem.Payload as global::Octokit.PullRequestEventPayload;
                        activityItem.IsPullRequestEvent = true;
                        break;
                    case "PullRequestReviewEvent":
                        activityItem.PullRequestReviewEventPayload = eventItem.Payload as global::Octokit.PullRequestReviewEventPayload;
                        activityItem.IsPullRequestReviewEvent = true;
                        break;
                    case "PushEvent":
                        activityItem.PushEventPayload = eventItem.Payload as global::Octokit.PushEventPayload;
                        activityItem.IsPushEvent = true;
                        break;
                    case "ReleaseEvent":
                        activityItem.ReleaseEventPayload = eventItem.Payload as global::Octokit.ReleaseEventPayload;
                        activityItem.IsReleaseEvent = true;
                        break;
                    case "StatusEvent":
                        activityItem.StatusEventPayload = eventItem.Payload as global::Octokit.StatusEventPayload;
                        activityItem.IsStatusEvent = true;
                        break;
                    case "WatchEvent":
                        activityItem.StarredEventPayload = eventItem.Payload as global::Octokit.StarredEventPayload;
                        activityItem.IsWatchEvent = true;
                        break;
                    case "unknown":
                        activityItem.ActivityPayload = eventItem.Payload as global::Octokit.ActivityPayload;
                        activityItem.IsUnknown = true;
                        break;
                }

                activityItem.CreatedAt = eventItem.CreatedAt;
                activityItem.Repository = eventItem.Repo;
                activityItem.Actor = eventItem.Actor;
                activityItem.Organization = eventItem.Org;

                activities.Add(activityItem);
            }

            return activities;
        }
    }
}