using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.UserControls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.UserControls.BlockButtons
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
			var item = ViewModel?.Item;
			if (item?.Repository?.Owner is null || item.Subject is null)
				return;

			var service = Ioc.Default.GetRequiredService<INavigationService>();

			var navBar = service.TabView.SelectedItem.NavigationBar;
			navBar.Context = new()
			{
				PrimaryText = item.Repository.Owner.Login,
				SecondaryText = item.Repository.Name,
				Number = item.Subject.Number,
			};

			switch (item.Subject.Type)
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
