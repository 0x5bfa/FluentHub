using GraphQL;

namespace FluentHub.Octokit.Queries.Users
{
    public class NotificationQueries
    {
        public async Task<List<Notification>> GetAllAsync(OctokitV3.NotificationsRequest request = null, OctokitV3.ApiOptions options = null)
        {
            var response = await App.Client.Activity.Notifications.GetAllForCurrent(request, options);

            Wrappers.NotificationWrapper wrapper = new();
            var notifications = wrapper.WrapAsync(response);

            var fragments = GetGetheredRepositoryFragment(notifications);

            var request2 = new GraphQLRequest
            {
                Query = @$"query {{ {fragments} }}",
            };

            var response2 = await App.GraphQLHttpClient.SendQueryAsync<NotificationSubjectDetails>(request2);
            List<Repository> zippedData = new()
            {
                response2.Data.repo0,
                response2.Data.repo1,
                response2.Data.repo2,
                response2.Data.repo3,
                response2.Data.repo4,
                response2.Data.repo5,
                response2.Data.repo6,
                response2.Data.repo7,
                response2.Data.repo8,
                response2.Data.repo9,
                response2.Data.repo10,
                response2.Data.repo11,
                response2.Data.repo12,
                response2.Data.repo13,
                response2.Data.repo14,
                response2.Data.repo15,
                response2.Data.repo16,
                response2.Data.repo17,
                response2.Data.repo18,
                response2.Data.repo19,
                response2.Data.repo20,
                response2.Data.repo21,
                response2.Data.repo22,
                response2.Data.repo23,
                response2.Data.repo24,
                response2.Data.repo25,
                response2.Data.repo26,
                response2.Data.repo27,
                response2.Data.repo28,
                response2.Data.repo29,
            };

            var mappedNotifications = Map(notifications, zippedData);

            return mappedNotifications;
        }

        private string GetGetheredRepositoryFragment(IReadOnlyList<Notification> notifications)
        {
            string getheredFragments = "";

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
                            getheredFragments += (issueFragment + "\n");
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
                            getheredFragments += (prFragment + "\n");
                            break;
                        }
                }

            }

            return getheredFragments;
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
                            break;
                        }
                    case NotificationSubjectType.PullRequest:
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
                            break;
                        }
                }

                item.Subject.TypeHumanized = item.Subject.Type.ToString();
                index++;
            }

            return notifications;
        }

        private class NotificationSubjectDetails
        {
            public Repository repo0 { get; set; }
            public Repository repo1 { get; set; }
            public Repository repo2 { get; set; }
            public Repository repo3 { get; set; }
            public Repository repo4 { get; set; }
            public Repository repo5 { get; set; }
            public Repository repo6 { get; set; }
            public Repository repo7 { get; set; }
            public Repository repo8 { get; set; }
            public Repository repo9 { get; set; }
            public Repository repo10 { get; set; }
            public Repository repo11 { get; set; }
            public Repository repo12 { get; set; }
            public Repository repo13 { get; set; }
            public Repository repo14 { get; set; }
            public Repository repo15 { get; set; }
            public Repository repo16 { get; set; }
            public Repository repo17 { get; set; }
            public Repository repo18 { get; set; }
            public Repository repo19 { get; set; }
            public Repository repo20 { get; set; }
            public Repository repo21 { get; set; }
            public Repository repo22 { get; set; }
            public Repository repo23 { get; set; }
            public Repository repo24 { get; set; }
            public Repository repo25 { get; set; }
            public Repository repo26 { get; set; }
            public Repository repo27 { get; set; }
            public Repository repo28 { get; set; }
            public Repository repo29 { get; set; }
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
