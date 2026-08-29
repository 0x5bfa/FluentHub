using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Controls.Overview;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls.Overview
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

		private async void OnRepoPageNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			var service = Ioc.Default.GetRequiredService<INavigationService>();
			if (ViewModel.Repository?.Owner?.Login is not { } owner || ViewModel.Repository.Name is not { } name)
				return;

			var repository = new RepositorySlug(owner, name);
			AppRoute route;

			switch (args.InvokedItemContainer?.Tag?.ToString()?.ToLowerInvariant())
			{
				default:
				case "code":
					var layout = App.AppSettings.UseDetailsView
						? RepositoryCodeLayout.Details
						: RepositoryCodeLayout.Tree;
					route = new RepositoryCodeRoute(repository, Layout: layout);
					break;
				case "issues":
					route = new RepositoryRoute(repository, RepositorySection.Issues);
					break;
				case "pullrequests":
					route = new RepositoryRoute(repository, RepositorySection.PullRequests);
					break;
				case "discussions":
					route = new RepositoryRoute(repository, RepositorySection.Discussions);
					break;
				case "projects":
					route = new RepositoryRoute(repository, RepositorySection.Projects);
					break;
				case "insights":
					route = new RepositoryRoute(repository, RepositorySection.Insights);
					break;
				case "settings":
					route = new RepositoryRoute(repository, RepositorySection.Settings);
					break;
			}

			await service.NavigateAsync(route);
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
