using FluentHub.Helpers;
using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using GraphQLModel = global::Octokit.GraphQL.Model;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class UserContributionGraphViewModel : INotifyPropertyChanged
    {
        private string loginName;
        public string LoginName { get => loginName; set => SetProperty(ref loginName, value); }

        private ContributionCalender calendar;
        public ContributionCalender Calendar { get => calendar; set => SetProperty(ref calendar, value); }

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
                    case GraphQLModel.ContributionLevel.None:
                        Calendar.ContributionDays[i].Color = "#64000000";
                        break;
                    case GraphQLModel.ContributionLevel.FirstQuartile:
                        Calendar.ContributionDays[i].Color = "#0e4429";
                        break;
                    case GraphQLModel.ContributionLevel.SecondQuartile:
                        Calendar.ContributionDays[i].Color = "#006d32";
                        break;
                    case GraphQLModel.ContributionLevel.ThirdQuartile:
                        Calendar.ContributionDays[i].Color = "#26a641";
                        break;
                    case GraphQLModel.ContributionLevel.FourthQuartile:
                        Calendar.ContributionDays[i].Color = "#39d353";
                        break;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
