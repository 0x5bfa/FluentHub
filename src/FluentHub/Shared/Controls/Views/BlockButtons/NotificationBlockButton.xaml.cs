using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Shared.Controls.ViewModels.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Shared.Controls.Views.BlockButtons
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

		private async void OnClick(object sender, RoutedEventArgs e)
		{
			var item = ViewModel?.Item;
			if (item?.Repository?.Owner is null || item.Subject is null)
				return;

			var service = Ioc.Default.GetRequiredService<INavigationService>();

			var repository = new RepositorySlug(item.Repository.Owner.Login, item.Repository.Name);

			switch (item.Subject.Type)
			{
				case NotificationSubjectType.IssueClosedAsCompleted:
				case NotificationSubjectType.IssueClosedAsNotPlanned:
				case NotificationSubjectType.IssueOpen:
					await service.NavigateAsync(new RepositoryIssueRoute(repository, item.Subject.Number));
					break;
				case NotificationSubjectType.PullRequestOpen:
				case NotificationSubjectType.PullRequestClosed:
				case NotificationSubjectType.PullRequestMerged:
				case NotificationSubjectType.PullRequestDraft:
					await service.NavigateAsync(new RepositoryPullRequestRoute(repository, item.Subject.Number));
					break;
			}
		}
	}
}
