using FluentHub.Services;
using FluentHub.ViewModels.Repositories;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Views.Repositories.Insights
{
	public sealed partial class InsightsPage : ScreenView
	{
		public InsightsPage()
		{
			InitializeComponent();

			navigationService = GetRequiredService<INavigationService>();
		}

		private readonly INavigationService navigationService;

		protected override void OnActivated(AppRoute route)
		{
			var chrome = navigationService.TabView.SelectedItem?.Chrome;
			if (chrome is null)
				return;

			chrome.Header = "Insights";
			chrome.Description = "Insights";
			chrome.Icon = new ImageIconSource
			{
				ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Insights.png"))
			};

			OnInsightsNavViewItemSelected("overview");
		}

		private void OnInsightsNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			if (args.InvokedItemContainer?.Tag is { } tag)
				OnInsightsNavViewItemSelected(tag.ToString()!.ToLowerInvariant());
		}

		private void OnInsightsNavViewItemSelected(string tag)
		{
			//string newUrl = $"{App.DefaultGitHubDomain}/{ViewModel.Repository.Owner.Login}/{ViewModel.Repository.Name}";

			switch (tag.ToLower())
			{
				default:
				case "overview":
					InsightsContentPresenter.Content = new OverviewPage();
					break;
				case "contributors":
					InsightsContentPresenter.Content = new ContributorsPage();
					break;
				case "traffic":
					InsightsContentPresenter.Content = new TrafficPage();
					break;
				case "commits":
					InsightsContentPresenter.Content = new CommitsPage();
					break;
				case "codefrequency":
					InsightsContentPresenter.Content = new CodeFrequencyPage();
					break;
			}
		}
	}
}
