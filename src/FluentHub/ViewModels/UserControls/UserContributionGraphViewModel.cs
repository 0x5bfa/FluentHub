using FluentHub.Core.Queries.Users;
using FluentHub.Helpers;
using FluentHub.Utils;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using FluentHub.Core.Application;
using FluentHub.Core.Contracts;
using FluentHub.Core.Models;

namespace FluentHub.ViewModels.UserControls
{
	public class UserContributionGraphViewModel : ObservableObject
	{
		private readonly IFluentHubGitHubClient _gitHub;

		public UserContributionGraphViewModel(IFluentHubGitHubClient gitHub)
		{
			_gitHub = gitHub;
			_mergedCalendar = new();
			MergedCalendar = new(_mergedCalendar);
		}

		private string _login = default!;
		public string Login { get => _login; set => SetProperty(ref _login, value); }

		private ContributionCalendar _calendar = default!;
		public ContributionCalendar Calendar { get => _calendar; set => SetProperty(ref _calendar, value); }

		private readonly ObservableCollection<ContributionCalendarItem> _mergedCalendar;
		public ReadOnlyObservableCollection<ContributionCalendarItem> MergedCalendar { get; }

		public async Task GetContributionCalendarAsync()
		{
			var queries = _gitHub.Users.Activities;
			var response = await queries.GetContributionCalendarAsync(Login);

			Calendar = response;
			_mergedCalendar.Clear();
			foreach (var item in ContributionCalendarService.CreateItems(response))
			{
				if (item.IsValid)
					item.Color = GetProperColor(item.ContributionLevel);

				_mergedCalendar.Add(item);
			}
		}

		public string GetProperColor(ContributionLevel level)
		{
			if (ThemeHelpers.RootTheme.ToString().ToLower() == "light")
			{
				return level switch
				{
					ContributionLevel.None =>		   "#EBEDF0",
					ContributionLevel.FirstQuartile =>  "#9BE9A8",
					ContributionLevel.SecondQuartile => "#40C463",
					ContributionLevel.ThirdQuartile =>  "#30A14E",
					ContributionLevel.FourthQuartile => "#216E39",
					_ => "#EBEDF0",
				};
			}
			else // dark
			{
				return level switch
				{
					ContributionLevel.None => GetEmptyContributionColor(),
					ContributionLevel.FirstQuartile =>  "#0E4429",
					ContributionLevel.SecondQuartile => "#006D32",
					ContributionLevel.ThirdQuartile =>  "#26A641",
					ContributionLevel.FourthQuartile => "#39D353",
					_ => GetEmptyContributionColor(),
				};
			}
		}

		private static string GetEmptyContributionColor()
			=> Application.Current.Resources["CardBackgroundFillColorSecondaryBrush"] is SolidColorBrush brush
				? brush.Color.ToString()
				: "#161B22";
	}
}
