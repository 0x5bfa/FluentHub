using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.Blocks
{
    public class UserContributionGraphViewModel : ObservableObject
    {
        private string loginName;
        public string LoginName { get => loginName; set => SetProperty(ref loginName, value); }

        private ContributionCalendar calendar;
        public ContributionCalendar Calendar { get => calendar; set => SetProperty(ref calendar, value); }

        public async Task GetContributionCalendarAsync()
        {
            ActivityQueries queries = new();
            Calendar = await queries.GetContributionCalendarAsync(LoginName);

            if (Calendar.ContributionDays[0].WeekDay != 0)
            {
                for (int i = 0; i < Calendar.ContributionDays[0].WeekDay; i++)
                {
                    CalendarDay day = new()
                    {
                        Color = "#FFFFFF",
                        ContributionCount = 0,
                        ContributionLevel = global::Octokit.GraphQL.Model.ContributionLevel.None,
                        Visibility = false,
                        WeekDay = 0,
                    };

                    Calendar.ContributionDays.Insert(i, day);
                }
            }

            if (ThemeHelper.ActualTheme.ToString().ToLower() == "dark")
            {
                SetProperColor();
            }
        }

        public void SetProperColor()
        {
            for (int i = 0; i < Calendar.ContributionDays.Count(); i++)
            {
                switch (Calendar.ContributionDays[i].ContributionLevel)
                {
                    case ContributionLevel.None:
                        Calendar.ContributionDays[i].Color = "#64000000";
                        break;
                    case ContributionLevel.FirstQuartile:
                        Calendar.ContributionDays[i].Color = "#0e4429";
                        break;
                    case ContributionLevel.SecondQuartile:
                        Calendar.ContributionDays[i].Color = "#006d32";
                        break;
                    case ContributionLevel.ThirdQuartile:
                        Calendar.ContributionDays[i].Color = "#26a641";
                        break;
                    case ContributionLevel.FourthQuartile:
                        Calendar.ContributionDays[i].Color = "#39d353";
                        break;
                }
            }
        }
    }
}
