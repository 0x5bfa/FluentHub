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

            if (Calendar.Weeks.First().ContributionDays.First().Weekday != 0)
            {
                for (int i = 0; i < 7 - Calendar.Weeks.First().ContributionDays.First().Weekday; i++)
                {
                    ContributionCalendarDay day = new()
                    {
                        Color = "#FFFFFF",
                        ContributionCount = 0,
                        ContributionLevel = ContributionLevel.None,
                        Weekday = 0,
                    };

                    Calendar.Weeks.First().ContributionDays.Insert(i, day);
                }
            }

            if (ThemeHelper.ActualTheme.ToString().ToLower() == "dark")
            {
                SetProperColor();
            }
        }

        public void SetProperColor()
        {
            for (int i = 0; i < Calendar.Weeks.First().ContributionDays.Count(); i++)
            {
                switch (Calendar.Weeks.First().ContributionDays[i].ContributionLevel)
                {
                    case ContributionLevel.None:
                        Calendar.Weeks.First().ContributionDays[i].Color = "#64000000";
                        break;
                    case ContributionLevel.FirstQuartile:
                        Calendar.Weeks.First().ContributionDays[i].Color = "#0e4429";
                        break;
                    case ContributionLevel.SecondQuartile:
                        Calendar.Weeks.First().ContributionDays[i].Color = "#006d32";
                        break;
                    case ContributionLevel.ThirdQuartile:
                        Calendar.Weeks.First().ContributionDays[i].Color = "#26a641";
                        break;
                    case ContributionLevel.FourthQuartile:
                        Calendar.Weeks.First().ContributionDays[i].Color = "#39d353";
                        break;
                }
            }
        }
    }
}
