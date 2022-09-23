using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHub.Octokit.Wrappers
{
    internal class NotificationWrapper
    {
        public List<Notification> WrapAsync(IReadOnlyList<OctokitV3.Notification> response)
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
                {
                    indivisual.LastReadAt = DateTimeOffset.Parse(item.LastReadAt);
                    indivisual.LastReadAtHumanized = indivisual.LastReadAt.Humanize();
                }

                if (item.UpdatedAt != null)
                {
                    indivisual.UpdatedAt = DateTimeOffset.Parse(item.UpdatedAt);
                    indivisual.UpdatedAtHumanized = indivisual.UpdatedAt.Humanize();
                }

                indivisual.Reason = item.Reason switch
                {
                    "assign" => "You were assigned to the issue.",
                    "author" => "You created the thread.",
                    "comment" => "You commented on the thread.",
                    "ci_activity" => "A workflow that you triggered was successful.",
                    "invitation" => "You accepted an invitation to contribute to the repository.",
                    "manual" => "You subscribed to the thread.",
                    "mention" => "You were mentioned.",
                    "review_requested" => "You or a team you are a member of was requested to review a pull request.",
                    "security_alert" => "A vulnerability was detected in your repository.",
                    "state_change" => "You changed the state of the thread.",
                    "subscribed" => "You started watching the repository.",
                    "team_mention" => "You are on a team that was mentioned.",
                    "" => "",
                };

                var urlItems = item.Subject.Url?.Split('/').ToList();

                switch (item.Subject?.Type)
                {
                    case "Issue":
                        {
                            indivisual.Subject.Type = NotificationSubjectType.Issue;
                            indivisual.Subject.Number = Convert.ToInt32(urlItems.LastOrDefault());
                            break;
                        }
                    case "PullRequest":
                        {
                            indivisual.Subject.Type = NotificationSubjectType.PullRequest;
                            indivisual.Subject.Number = Convert.ToInt32(urlItems.LastOrDefault());
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
