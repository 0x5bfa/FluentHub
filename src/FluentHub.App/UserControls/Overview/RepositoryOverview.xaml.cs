using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.Overview
{
	public sealed partial class RepositoryOverview : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(ViewModel),
				typeof(RepositoryOverviewViewModel),
				typeof(RepositoryOverviewViewModel),
				new PropertyMetadata(null));

		public RepositoryOverviewViewModel ViewModel
		{
			get => (RepositoryOverviewViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				if (ViewModel is not null)
					SelectItemByTag(ViewModel.SelectedTag);
			}
		}
		#endregion

		public RepositoryOverview() => InitializeComponent();

		private void OnRepoPageNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			var service = Ioc.Default.GetRequiredService<INavigationService>();

			switch (args.InvokedItemContainer?.Tag?.ToString().ToLower())
			{
				default:
				case "code":
					var param = new FrameNavigationParameter()
					{
						PrimaryText = ViewModel.Repository?.Owner?.Login,
						SecondaryText = ViewModel.Repository?.Name,
					};

					if (App.AppSettings.UseDetailsView)
						service.Navigate<Views.Repositories.Code.DetailsLayoutView>(param);
					else
						service.Navigate<Views.Repositories.Code.TreeLayoutView>(param);
					break;
				case "issues":
					service.Navigate(
						typeof(Views.Repositories.Issues.IssuesPage),
						new FrameNavigationParameter()
						{
							PrimaryText = ViewModel.Repository?.Owner?.Login,
							SecondaryText = ViewModel.Repository?.Name,
						});
					break;
				case "pullrequests":
					service.Navigate(
						typeof(Views.Repositories.PullRequests.PullRequestsPage),
						new FrameNavigationParameter()
						{
							PrimaryText = ViewModel.Repository?.Owner?.Login,
							SecondaryText = ViewModel.Repository?.Name,
						});
					break;
				case "discussions":
					service.Navigate(
						typeof(Views.Repositories.Discussions.DiscussionsPage),
						new FrameNavigationParameter()
						{
							PrimaryText = ViewModel.Repository?.Owner?.Login,
							SecondaryText = ViewModel.Repository?.Name,
						});
					break;
				case "projects":
					service.Navigate(
						typeof(Views.Repositories.Projects.ProjectsPage),
						new FrameNavigationParameter()
						{
							PrimaryText = ViewModel.Repository?.Owner?.Login,
							SecondaryText = ViewModel.Repository?.Name,
						});
					break;
				case "insights":
					service.Navigate(typeof(Views.Repositories.Insights.InsightsPage));
					break;
				case "settings":
					service.Navigate(typeof(Views.Repositories.Settings.SettingsPage));
					break;
			}
		}

		private void SelectItemByTag(string tag)
		{
			var defaultItem
				= RepoPageNavView
				.MenuItems
				.OfType<NavigationViewItem>()
				.FirstOrDefault();

			RepoPageNavView.SelectedItem
				= RepoPageNavView
				.MenuItems
				.OfType<NavigationViewItem>()
				.FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
				?? defaultItem;
		}
	}
}
