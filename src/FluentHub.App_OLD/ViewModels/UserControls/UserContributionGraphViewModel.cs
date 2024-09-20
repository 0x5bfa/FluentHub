using FluentHub.App.Models;
using FluentHub.Octokit.Queries.Users;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace FluentHub.App.ViewModels.UserControls
{
	public class UserContributionGraphViewModel : ObservableObject
	{
		public UserContributionGraphViewModel()
		{
			_mergedCalendar = new();
			MergedCalendar = new(_mergedCalendar);
		}

		private string _login;
		public string Login { get => _login; set => SetProperty(ref _login, value); }

		private ContributionCalendar _calendar;
		public ContributionCalendar Calendar { get => _calendar; set => SetProperty(ref _calendar, value); }

		private readonly ObservableCollection<MergedCalendarDays> _mergedCalendar;
		public ReadOnlyObservableCollection<MergedCalendarDays> MergedCalendar { get; }

		public async Task GetContributionCalendarAsync()
		{
			ActivityQueries queries = new();
			var response = await queries.GetContributionCalendarAsync(Login);

			Calendar = response;

			// Flatting
			foreach (var weekItem in response.Weeks)
			{
				foreach (var dayItem in weekItem.ContributionDays)
				{
					var item = new MergedCalendarDays()
					{
						Color = dayItem.Color,
						ContributionCount = dayItem.ContributionCount,
						ContributionLevel = dayItem.ContributionLevel,
						Weekday = dayItem.Weekday,
						IsVaild = true,
					};

					item.Color = GetProperColor(item.ContributionLevel);

					_mergedCalendar.Add(item);
				}
			}

			int weekDay = _mergedCalendar.FirstOrDefault().Weekday;

			// If the first day is not Sunday (Weekday != 0), fill in the vacancies to correct the position.
			if (weekDay != 0)
			{
				for (int index = 0; index < weekDay; index++)
				{
					_mergedCalendar.Insert(
						0,
						new()
						{
							Color = "#FFFFFF",
							ContributionCount = 0,
							ContributionLevel = ContributionLevel.None,
							Weekday = weekDay - (index + 1), // prevent the value of Weekday from descending order.
							IsVaild = false,
						});
				}
			}
		}

		public string GetProperColor(ContributionLevel level)
		{
			if (ThemeHelpers.RootTheme.ToString().ToLower() == "light")
			{
				return level switch
				{
					ContributionLevel.None => "#EBEDF0",
					ContributionLevel.FirstQuartile => "#9BE9A8",
					ContributionLevel.SecondQuartile => "#40C463",
					ContributionLevel.ThirdQuartile => "#30A14E",
					ContributionLevel.FourthQuartile => "#216E39",
					_ => "#EBEDF0",
				};
			}
			else // dark
			{
				return level switch
				{
					ContributionLevel.None => (Application.Current.Resources["PrimerScaleGray8"] as SolidColorBrush).Color.ToString(),
					ContributionLevel.FirstQuartile => "#0E4429",
					ContributionLevel.SecondQuartile => "#006D32",
					ContributionLevel.ThirdQuartile => "#26A641",
					ContributionLevel.FourthQuartile => "#39D353",
					_ => (Application.Current.Resources["PrimerScaleGray8"] as SolidColorBrush).Color.ToString(),
				};
			}
		}
	}
}
