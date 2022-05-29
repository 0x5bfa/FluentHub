using FluentHub.Octokit.Models.ActivityPayloads;

namespace FluentHub.Octokit.Queries.Users
{
    public class ActivityQueries
    {
        public async Task<List<Activity>> GetAllAsync(string login)
        {
            #region query
            OctokitOriginal.ApiOptions options = new()
            {
                PageCount = 1,
                PageSize = 60,
                StartPage = 1
            };

            var response = await App.Client.Activity.Events.GetAllUserReceived(login, options);
            #endregion

            #region copying
            List<Activity> activities = new();

            foreach (var item in response)
            {
                Activity indivisual = new()
                {
                    Payload = item.Payload,
                    PayloadType = item.Type,
                    CreatedAt = item.CreatedAt,
                    CreatedAtHumanized = item.CreatedAt.Humanize(),
                    Repository = new()
                    {
                        Name = item.Repo?.Name.Split('/')[1],
                        Owner = new()
                        {
                            Login = item.Repo?.Name.Split('/')[0],
                        },
                    },
                    Actor = new()
                    {
                        AvatarUrl = item.Actor.AvatarUrl,
                        Login = item.Actor.Login,
                        Name  = item.Actor.Name,
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
                        break;
                    case "CheckSuiteEvent":
                        break;
                    case "CommitComment":
                        break;
                    case "CreateEvent":
                        var createEventPayload = (OctokitOriginal.CreateEventPayload)item.Payload;
                        indivisual.CreateEventPayload = new()
                        {
                            Description = createEventPayload.Description,
                            MasterBranch = createEventPayload.MasterBranch,
                            Ref = createEventPayload.Ref,
                            RefType = createEventPayload.RefType,
                        };
                        break;
                    case "DeleteEvent":
                        var deleteEventPayload = (OctokitOriginal.DeleteEventPayload)item.Payload;
                        indivisual.DeleteEventPayload = new()
                        {
                            Ref = deleteEventPayload.Ref,
                            RefType = deleteEventPayload.RefType,
                        };
                        break;
                    case "ForkEvent":
                        var forkEventPayload = (OctokitOriginal.ForkEventPayload)item.Payload;
                        indivisual.ForkEventPayload = new()
                        {
                            Forkee = forkEventPayload.Forkee,
                        };
                        break;
                    case "IssueCommentEvent":
                        var issueCommentPayload = (OctokitOriginal.IssueCommentPayload)item.Payload;
                        indivisual.IssueCommentPayload = new()
                        {
                            Action = issueCommentPayload.Action,
                            Comment = issueCommentPayload.Comment,
                            Issue = issueCommentPayload.Issue,
                        };
                        break;
                    case "IssueEvent":
                        break;
                    case "PullRequestComment":
                        break;
                    case "PullRequestEvent":
                        break;
                    case "PullRequestReviewEvent":
                        break;
                    case "PushEvent":
                        var pushEventPayload = (OctokitOriginal.PushEventPayload)item.Payload;
                        indivisual.PushEventPayload = new()
                        {
                            Commits = pushEventPayload.Commits,
                            Head = pushEventPayload.Head,
                            Ref = pushEventPayload.Ref,
                            Size = pushEventPayload.Size,
                        };
                        break;
                    case "PushWebhookCommit":
                        break;
                    case "PushWebhookCommitter":
                        break;
                    case "PushWebhook":
                        break;
                    case "ReleaseEvent":
                        var releaseEventPayload = (OctokitOriginal.ReleaseEventPayload)item.Payload;
                        indivisual.ReleaseEventPayload = new()
                        {
                            Action = releaseEventPayload.Action,
                            Release = releaseEventPayload.Release,
                        };
                        break;
                    case "WatchEvent":
                        var watchEventPayload = (OctokitOriginal.StarredEventPayload)item.Payload;
                        indivisual.StarredEventPayload = new()
                        {
                            Action = watchEventPayload.Action,
                        };
                        break;
                    case "StatusEvent":
                        break;
                }

                activities.Add(indivisual);
            }
            #endregion

            return activities;
        }

        public async Task<ContributionCalender> GetContributionCalendarAsync(string login)
        {
            #region queries
            var query = new Query()
                .User(login: login)
                .ContributionsCollection(null, null, null)
                .ContributionCalendar
                .Select(x => new
                {
                    x.TotalContributions,
                    A = x.Weeks.Select(y => new
                    {
                        B = y.ContributionDays.Select(z => new
                        {
                            z.Color,
                            z.ContributionCount,
                            z.ContributionLevel,
                            z.Weekday,
                        })
                        .ToList(),
                    })
                    .ToList(),
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            #region copying
            var item = new ContributionCalender();

            item.TotalContributions = response.TotalContributions;

            foreach (var weeks in response.A)
            {
                foreach (var days in weeks.B)
                {
                    CalendarDay day = new();

                    day.Color = days.Color;
                    day.ContributionCount = days.ContributionCount;
                    day.ContributionLevel = days.ContributionLevel;
                    day.WeekDay = days.Weekday;
                    day.Visibility = true;

                    item.ContributionDays.Add(day);
                }
            }
            #endregion

            return item;
        }
    }
}
