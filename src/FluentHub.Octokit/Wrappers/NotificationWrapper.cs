using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHub.Octokit.Wrappers
{
    internal class NotificationWrapper
    {
        public async Task<List<Notification>> WrapAsync(IReadOnlyList<OctokitOriginal.Notification> response)
        {
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
                    indivisual.LastReadAt = DateTimeOffset.Parse(item.LastReadAt);

                if (item.UpdatedAt != null)
                    indivisual.UpdatedAt = DateTimeOffset.Parse(item.UpdatedAt);

                switch (item.Reason)
                {
                    case "assign":
                        indivisual.Reason = "You were assigned to the issue.";
                        break;
                    case "author":
                        indivisual.Reason = "You created the thread.";
                        break;
                    case "comment":
                        indivisual.Reason = "You commented on the thread.";
                        break;
                    case "ci_activity":
                        indivisual.Reason = "A workflow that you triggered was successful.";
                        break;
                    case "invitation":
                        indivisual.Reason = "You accepted an invitation to contribute to the repository.";
                        break;
                    case "manual":
                        indivisual.Reason = "You subscribed to the thread.";
                        break;
                    case "mention":
                        indivisual.Reason = "You were mentioned.";
                        break;
                    case "review_requested":
                        indivisual.Reason = "You or a team you are a member of was requested to review a pull request.";
                        break;
                    case "security_alert":
                        indivisual.Reason = "A vulnerability was detected in your repository.";
                        break;
                    case "state_change":
                        indivisual.Reason = "You changed the state of the thread.";
                        break;
                    case "subscribed":
                        indivisual.Reason = "You started watching the repository.";
                        break;
                    case "team_mention":
                        indivisual.Reason = "You are on a team that was mentioned.";
                        break;
                    default:
                        indivisual.Reason = "";
                        break;
                }

                var urlItems = item.Subject.Url?.Split('/');

                switch (item.Subject?.Type)
                {
                    case "Issue":
                        {
                            Queries.Repositories.IssueQueries issueQueries = new();
                            var issue = await issueQueries.GetAsync(
                                indivisual.Repository.Owner.Login,
                                indivisual.Repository.Name,
                                Convert.ToInt32(urlItems[urlItems.Count() - 1]));

                            if (issue.Closed)
                            {
                                indivisual.Subject.Type = NotificationSubjectType.IssueClosedAsCompleted;
                            }
                            else
                            {
                                indivisual.Subject.Type = NotificationSubjectType.IssueOpen;
                            }

                            indivisual.Subject.Number = issue.Number;

                            break;
                        }
                    case "PullRequest":
                        {
                            Queries.Repositories.PullRequestQueries pullQueries = new();
                            var pull = await pullQueries.GetAsync(
                                indivisual.Repository.Owner.Login,
                                indivisual.Repository.Name,
                                Convert.ToInt32(urlItems[urlItems.Count() - 1]));

                            if (pull.Closed)
                            {
                                if (pull.Merged)
                                {
                                    indivisual.Subject.Type = NotificationSubjectType.PullRequestMerged;
                                }
                                else
                                {
                                    indivisual.Subject.Type = NotificationSubjectType.PullRequestClosed;
                                }
                            }
                            else
                            {
                                if (pull.IsDraft)
                                {
                                    indivisual.Subject.Type = NotificationSubjectType.PullRequestDraft;
                                }
                                else
                                {
                                    indivisual.Subject.Type = NotificationSubjectType.PullRequestOpen;
                                }
                            }

                            indivisual.Subject.Number = pull.Number;

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

            return notifications;
        }
    }
}
