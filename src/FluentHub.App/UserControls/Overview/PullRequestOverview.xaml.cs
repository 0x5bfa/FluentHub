using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.Overview
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

		private void OnPullRequestNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			var service = Ioc.Default.GetRequiredService<INavigationService>();

			var param = new FrameNavigationParameter()
			{
				PrimaryText = ViewModel.PullRequest?.Repository?.Owner?.Login,
				SecondaryText = ViewModel.PullRequest?.Repository?.Name,
				Number = ViewModel.PullRequest.Number,
			};

			switch (args.InvokedItemContainer.Tag.ToString().ToLower())
			{
				default:
				case "conversation":
					service.Navigate<Views.Repositories.PullRequests.ConversationPage>(param);
					break;
				case "commits":
					service.Navigate<Views.Repositories.PullRequests.CommitsPage>(param);
					break;
				case "checks":
					service.Navigate<Views.Repositories.PullRequests.ChecksPage>(param);
					break;
				case "filechanges":
					service.Navigate<Views.Repositories.PullRequests.FileChangesPage>(param);
					break;
			}
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
