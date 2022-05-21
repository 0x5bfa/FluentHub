namespace FluentHub.Octokit.Queries.Users
{
    public class NotificationQueries
    {
        public NotificationQueries() => new App();

        public async Task<List<Notification>> GetAllAsync(OctokitOriginal.ApiOptions options = null)
        {
            OctokitOriginal.NotificationsRequest request = new()
            {
                All = true
            };

            if (options == null)
            {
                options = new()
                {
                    PageCount = 1,
                    PageSize = 30,
                    StartPage = 1
                };
            }

            var response = await App.Client.Activity.Notifications.GetAllForCurrent(request, options);

            List<Notification> notifications = new();
            foreach (var item in response)
            {
                Notification indivisual = new()
                {
                    Id = Convert.ToInt64(item.Id),
                    Reason = item.Reason,
                    Repository = item.Repository,
                    Subject = item.Subject,
                    Unread = item.Unread,
                    Url = item.Url,
                };

                if (item.LastReadAt != null)
                    indivisual.LastReadAt = DateTimeOffset.Parse(item.LastReadAt);

                if (item.UpdatedAt != null)
                    indivisual.UpdatedAt = DateTimeOffset.Parse(item.UpdatedAt);

                if (item.UpdatedAt != null)
                    indivisual.UpdatedAtHumanized = DateTimeOffset.Parse(item.UpdatedAt).Humanize();

                switch (item.Reason)
                {
                    case "assign":
                        indivisual.FullReason = "You were assigned to the issue.";
                        break;
                    case "author":
                        indivisual.FullReason = "You created the thread.";
                        break;
                    case "comment":
                        indivisual.FullReason = "You commented on the thread.";
                        break;
                    case "ci_activity":
                        indivisual.FullReason = "A GitHub Actions workflow run that you triggered was completed.";
                        break;
                    case "invitation":
                        indivisual.FullReason = "You accepted an invitation to contribute to the repository.";
                        break;
                    case "manual":
                        indivisual.FullReason = "You subscribed to the thread (via an issue or pull request).";
                        break;
                    case "mention":
                        indivisual.FullReason = "You were specifically @mentioned in the content.";
                        break;
                    case "review_requested":
                        indivisual.FullReason = "You, or a team you're a member of, were requested to review a pull request.";
                        break;
                    case "security_alert":
                        indivisual.FullReason = "GitHub discovered a security vulnerability in your repository.";
                        break;
                    case "state_change":
                        indivisual.FullReason = "You changed the thread state (for example, closing an issue or merging a pull request).";
                        break;
                    case "subscribed":
                        indivisual.FullReason = "You're watching the repository.";
                        break;
                    case "team_mention":
                        indivisual.FullReason = "You were on a team that was mentioned.";
                        break;
                    default:
                        indivisual.FullReason = "";
                        break;
                }

                var urlItems = indivisual.Subject.Url?.Split("/");

                switch (indivisual.Subject?.Type)
                {
                    case "Issue":

                        Repositories.IssueQueries issueQueries = new();
                        var issue = await issueQueries.GetAsync(
                            indivisual.Repository.Owner.Login,
                            indivisual.Repository.Name,
                            Convert.ToInt32(urlItems[urlItems.Count() - 1]));

                        if (issue.Closed)
                        {
                            indivisual.ItemState = "IssueClosed";
                        }
                        else
                        {
                            indivisual.ItemState = "IssueOpen";
                        }

                        indivisual.SubjectNumber = issue.Number;

                        break;

                    case "PullRequest":

                        Repositories.PullRequestQueries pullQueries = new();
                        var pull = await pullQueries.GetAsync(
                            indivisual.Repository.Owner.Login,
                            indivisual.Repository.Name,
                            Convert.ToInt32(urlItems[urlItems.Count() - 1]));

                        if (pull.Closed)
                        {
                            if (pull.Merged)
                            {
                                indivisual.ItemState = "PullMerged";
                            }
                            else
                            {
                                indivisual.ItemState = "PullClosed";
                            }
                        }
                        else
                        {
                            if (pull.IsDraft)
                            {
                                indivisual.ItemState = "PullDraft";
                            }
                            else
                            {
                                indivisual.ItemState = "PullOpen";
                            }
                        }

                        indivisual.SubjectNumber = pull.Number;

                        break;
                    case "Discussion":

                        indivisual.ItemState = "Discussion";

                        break;
                    case "Commit":

                        indivisual.ItemState = "Commit";

                        break;
                }

                notifications.Add(indivisual);
            }

            return notifications;
        }

        public async Task<int> GetUnreadCount()
        {
            OctokitOriginal.NotificationsRequest request = new()
            {
                All = true
            };

            OctokitOriginal.ApiOptions options = new()
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
