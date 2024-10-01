using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.BlockButtons
{
	public sealed partial class NotificationBlockButton : UserControl
	{
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(NotificationBlockButtonViewModel),
				typeof(NotificationBlockButtonViewModel),
				typeof(NotificationBlockButton),
				new PropertyMetadata(null));

		public NotificationBlockButtonViewModel ViewModel
		{
			get => (NotificationBlockButtonViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		public NotificationBlockButton()
		{
			InitializeComponent();
		}

		private void OnClick(object sender, RoutedEventArgs e)
		{
			var service = Ioc.Default.GetRequiredService<INavigationService>();

			var navBar = service.TabView.SelectedItem.NavigationBar;
			navBar.Context = new()
			{
				PrimaryText = ViewModel.Item.Repository.Owner.Login,
				SecondaryText = ViewModel.Item.Repository.Name,
				Number = ViewModel.Item.Subject.Number,
			};

			switch (ViewModel.Item.Subject.Type)
			{
				case NotificationSubjectType.IssueClosedAsCompleted:
				case NotificationSubjectType.IssueClosedAsNotPlanned:
				case NotificationSubjectType.IssueOpen:
					service.Navigate<Views.Repositories.Issues.IssuePage>();
					break;
				case NotificationSubjectType.PullRequestOpen:
				case NotificationSubjectType.PullRequestClosed:
				case NotificationSubjectType.PullRequestMerged:
				case NotificationSubjectType.PullRequestDraft:
					service.Navigate<Views.Repositories.PullRequests.ConversationPage>();
					break;
			}
		}
	}
}
