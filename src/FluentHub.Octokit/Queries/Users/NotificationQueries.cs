using FluentHub.Octokit.Models;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OctokitOriginal = global::Octokit;

namespace FluentHub.Octokit.Queries.Users
{
    public class NotificationQueries
    {
        public NotificationQueries() => new App();

        public async Task<List<Notification>> GetAllAsync()
        {
            OctokitOriginal.NotificationsRequest request = new()
            {
                All = true
            };

            OctokitOriginal.ApiOptions options = new()
            {
                PageCount = 1,
                PageSize = 30,
                StartPage = 1
            };

            var response = await App.Client.Activity.Notifications.GetAllForCurrent(request, options);

            List<Notification> notifications = new();
            foreach (var item in response)
            {
                Notification indivisual = new()
                {
                    Id = Convert.ToInt64(item.Id),
                    LastReadAt = DateTimeOffset.Parse(item.LastReadAt),
                    Reason = item.Reason,
                    Repository = item.Repository,
                    Subject = item.Subject,
                    Unread = item.Unread,
                    UpdatedAt = DateTimeOffset.Parse(item.UpdatedAt),
                    UpdatedAtHumanized = DateTimeOffset.Parse(item.UpdatedAt).Humanize(),
                    Url = item.Url,
                };

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
    }
}
