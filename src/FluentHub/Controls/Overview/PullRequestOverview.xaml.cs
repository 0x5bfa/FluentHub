using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Controls.Overview;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls.Overview
{
	public sealed partial class PullRequestOverview : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(ViewModel),
				typeof(PullRequestOverviewViewModel),
				typeof(PullRequestOverviewViewModel),
				new PropertyMetadata(null));

		public PullRequestOverviewViewModel ViewModel
		{
			get => (PullRequestOverviewViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				if (ViewModel is not null)
					SelectItemByTag(ViewModel.SelectedTag);
			}
		}
		#endregion

		public PullRequestOverview()
		{
			InitializeComponent();
		}

		private async void OnPullRequestNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			if (ViewModel?.PullRequest is not { } pullRequest || args.InvokedItemContainer?.Tag is not { } tag)
				return;

			var service = Ioc.Default.GetRequiredService<INavigationService>();

			if (pullRequest.Repository?.Owner?.Login is not { } owner || pullRequest.Repository.Name is not { } name)
				return;

			var repository = new RepositorySlug(owner, name);
			var section = PullRequestSection.Conversation;

			switch (tag.ToString()!.ToLowerInvariant())
			{
				default:
				case "conversation":
					section = PullRequestSection.Conversation;
					break;
				case "commits":
					section = PullRequestSection.Commits;
					break;
				case "checks":
					section = PullRequestSection.Checks;
					break;
				case "filechanges":
					section = PullRequestSection.Files;
					break;
			}

			await service.NavigateAsync(new RepositoryPullRequestRoute(repository, pullRequest.Number, section));
		}

		private void SelectItemByTag(string tag)
		{
			var defaultItem
				= PullRequestNavView
				.MenuItems
				.OfType<NavigationViewItem>()
				.FirstOrDefault();

			PullRequestNavView.SelectedItem
				= PullRequestNavView
				.MenuItems
				.OfType<NavigationViewItem>()
				.FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
				?? defaultItem;
		}
	}
}
