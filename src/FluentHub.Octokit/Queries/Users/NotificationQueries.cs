using GraphQL;
using Newtonsoft.Json.Linq;

namespace FluentHub.Octokit.Queries.Users
{
    public class NotificationQueries
    {
        public async Task<List<Notification>> GetAllAsync(OctokitV3.NotificationsRequest request = null, OctokitV3.ApiOptions options = null)
        {
            var response = await App.Client.Activity.Notifications.GetAllForCurrent(request, options);

            Wrappers.NotificationWrapper wrapper = new();
            var notifications = wrapper.WrapAsync(response);

            var fragments = GetGatheredRepositoryFragment(notifications);

            var request2 = new GraphQLRequest
            {
                Query = @$"query {{ {fragments} }}",
            };

            var response2 = await App.GraphQLHttpClient.SendQueryAsync<object>(request2);
            List<Repository> zippedData = new();

            var json = response2.Data as JToken;

            var errors = json["errors"];
            if (errors is not null)
            {
                return notifications;
            }
            for (int idx = 0; idx < options.PageSize; idx++)
            {
                var repo = json[$"repo{idx}"];
                if (repo is null || !repo.HasValues)
                {
                    // Add empty data
                    zippedData.Add(new());
                    continue;
                }

                var issue = repo["Issue"];
                var pr = repo["PullRequest"];

                //HasValues because issue can be empty
                if (issue is not null && issue.HasValues)
                {
                    Enum.TryParse(
                        issue["state"].ToString(),
                        true,
                        out IssueState state);
                    Enum.TryParse(
                        issue["stateReason"].ToString(),
                        true,
                        out IssueStateReason stateReason);
                    var id = new ID(
                        issue["id"].ToString());
                    int.TryParse(
                        issue["number"].ToString(),
                        out int number);

                    zippedData.Add(new()
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
                else if (pr is not null && pr.HasValues)
                {
                    Enum.TryParse(
                        pr["state"].ToString(),
                        true,
                        out PullRequestState state);
                    var id = new ID(
                        pr["id"].ToString());
                    int.TryParse(
                        pr["number"].ToString(),
                        out int number);
                    bool.TryParse(
                        pr["isDraft"].ToString(),
                        out bool isDraft);

                    zippedData.Add(new()
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
            }
            
            
            var slicedNotifications = new List<Notification>();
            for (int i = 0; i < zippedData.Count; i++)
            {
                slicedNotifications.Add(notifications[i]);
            }
            
            var mappedNotifications = Map(slicedNotifications, zippedData);

            return mappedNotifications;
        }

        private string GetGatheredRepositoryFragment(IReadOnlyList<Notification> notifications)
        {
            string gatheredFragments = "";
            for (int index = 0; index < notifications.Count; index++)
            {
                switch (notifications.ElementAt(index).Subject.Type)
                {
                    case NotificationSubjectType.Discussion:
                    case NotificationSubjectType.Commit:
                    case NotificationSubjectType.Release:
                        break;
                    case NotificationSubjectType.Issue:
                        {
                            var issueFragment =
                                @$"
repo{index}: repository(name: ""{notifications.ElementAt(index).Repository.Name}"", owner: ""{notifications.ElementAt(index).Repository.Owner.Login}"") {{
  Issue: issue(number: {notifications.ElementAt(index).Subject.Number}) {{
    id
    number
    state
    stateReason
  }}
}}";
                            gatheredFragments += (issueFragment + "\n");
                            break;
                        }
                    case NotificationSubjectType.PullRequest:
                        {
                            var prFragment =
                                @$"
repo{index}: repository(name: ""{notifications.ElementAt(index).Repository.Name}"", owner: ""{notifications.ElementAt(index).Repository.Owner.Login}"") {{
  PullRequest: pullRequest(number: {notifications.ElementAt(index).Subject.Number}) {{
    id
    number
    isDraft
    state
  }}
}}";
                            gatheredFragments += (prFragment + "\n");
                            break;
                        }
                }
            }

            return gatheredFragments;
        }

        private List<Notification> Map(List<Notification> notifications, IReadOnlyList<Repository> details)
        {
            int index = 0;
            foreach (var item in notifications)
            {
                switch (item.Subject.Type)
                {
                    case NotificationSubjectType.Discussion:
                    case NotificationSubjectType.Commit:
                    case NotificationSubjectType.Release:
                        break;
                    case NotificationSubjectType.Issue:
                        {
                            if (details.ElementAt(index).Issue is not null)
                            {
                                item.Subject.Number = details.ElementAt(index).Issue.Number;

                                switch (details.ElementAt(index).Issue.State)
                                {
                                    case IssueState.Open:
                                    {
                                        item.Subject.Type = NotificationSubjectType.IssueOpen;
                                        break;
                                    }
                                    case IssueState.Closed:
                                    {
                                        switch (details.ElementAt(index).Issue.StateReason)
                                        {
                                            case IssueStateReason.Completed:
                                                item.Subject.Type = NotificationSubjectType.IssueClosedAsCompleted;
                                                break;
                                            case IssueStateReason.NotPlanned:
                                                item.Subject.Type = NotificationSubjectType.IssueClosedAsNotPlanned;
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
                            if (details.ElementAt(index).PullRequest is not null)
                            {
                                item.Subject.Number = details.ElementAt(index).PullRequest.Number;

                                switch (details.ElementAt(index).PullRequest.State)
                                {
                                    case PullRequestState.Open:
                                    {
                                        item.Subject.Type = details.ElementAt(index).PullRequest.IsDraft ?
                                            NotificationSubjectType.PullRequestDraft :
                                            NotificationSubjectType.PullRequestOpen;
                                        break;
                                    }
                                    case PullRequestState.Closed:
                                    {
                                        item.Subject.Type = NotificationSubjectType.PullRequestClosed;
                                        break;
                                    }
                                    case PullRequestState.Merged:
                                    {
                                        item.Subject.Type = NotificationSubjectType.PullRequestMerged;
                                        break;
                                    }
                                }
                            }

                            break;
                        } 
                        index++;
                }

                item.Subject.TypeHumanized = item.Subject.Type.ToString();
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
