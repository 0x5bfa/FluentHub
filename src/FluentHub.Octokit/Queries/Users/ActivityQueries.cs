using FluentHub.Octokit.Models;
using Humanizer;
using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OctokitOriginal = global::Octokit;

namespace FluentHub.Octokit.Queries.Users
{
    public class ActivityQueries
    {
        public ActivityQueries() => new App();

        public async Task<List<Activity>> GetAllAsync(string login)
        {
            #region query
            OctokitOriginal.ApiOptions options = new()
            {
                PageCount = 1,
                PageSize = 30,
                StartPage = 1
            };

            var response = await App.Client.Activity.Events.GetAllUserReceived(login, options);
            #endregion

            #region copying
            List<Activity> activities = new();

            foreach (var item in response)
            {
                Activity activityItem = new()
                {
                    Payload = item.Payload,
                    PayloadType = item.Type,
                    CreatedAt = item.CreatedAt,
                    CreatedAtHumanized = item.CreatedAt.Humanize(),
                    Repository = item.Repo,
                    Actor = item.Actor,
                    Organization = item.Org,
                };

                activities.Add(activityItem);
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
