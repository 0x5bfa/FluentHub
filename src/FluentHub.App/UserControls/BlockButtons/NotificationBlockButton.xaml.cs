using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.BlockButtons
{
    public sealed partial class NotificationBlockButton : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(NotificationBlockButtonViewModel),
                  typeof(NotificationBlockButtonViewModel),
                  typeof(NotificationBlockButton),
                  new PropertyMetadata(null)
                );

        public NotificationBlockButtonViewModel ViewModel
        {
            get => (NotificationBlockButtonViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        #endregion

        public NotificationBlockButton()
            => InitializeComponent();

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var navService = Ioc.Default.GetRequiredService<INavigationService>();

            switch (ViewModel.Item.Subject.Type)
            {
                case NotificationSubjectType.IssueClosedAsCompleted:
                case NotificationSubjectType.IssueClosedAsNotPlanned:
                case NotificationSubjectType.IssueOpen:
                    navService.Navigate<Views.Repositories.Issues.IssuePage>(
                    new FrameNavigationParameter()
                    {
                        UserLogin = ViewModel.Item.Repository.Owner.Login,
                        RepositoryName = ViewModel.Item.Repository.Name,
                        Number = ViewModel.Item.Subject.Number,
                    });
                    break;
                case NotificationSubjectType.PullRequestOpen:
                case NotificationSubjectType.PullRequestClosed:
                case NotificationSubjectType.PullRequestMerged:
                case NotificationSubjectType.PullRequestDraft:
                    navService.Navigate<Views.Repositories.PullRequests.ConversationPage>(
                    new FrameNavigationParameter()
                    {
                        UserLogin = ViewModel.Item.Repository.Owner.Login,
                        RepositoryName = ViewModel.Item.Repository.Name,
                        Number = ViewModel.Item.Subject.Number,
                    });
                    break;
            }
        }
    }
}
