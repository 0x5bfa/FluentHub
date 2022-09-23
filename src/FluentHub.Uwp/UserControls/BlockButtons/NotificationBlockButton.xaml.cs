using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.UserControls.BlockButtons;
using FluentHub.Uwp.Views.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.BlockButtons
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

        public NotificationBlockButton() => InitializeComponent();

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var navService = App.Current.Services.GetRequiredService<INavigationService>();

            switch (ViewModel.Item.Subject.Type)
            {
                case NotificationSubjectType.IssueClosedAsCompleted:
                case NotificationSubjectType.IssueClosedAsNotPlanned:
                case NotificationSubjectType.IssueOpen:
                    navService.Navigate<Views.Repositories.Issues.IssuePage>(
                    new FrameNavigationArgs()
                    {
                        Login = ViewModel.Item.Repository.Owner.Login,
                        Name = ViewModel.Item.Repository.Name,
                        Number = ViewModel.Item.Subject.Number,
                    });
                    break;
                case NotificationSubjectType.PullRequestOpen:
                case NotificationSubjectType.PullRequestClosed:
                case NotificationSubjectType.PullRequestMerged:
                case NotificationSubjectType.PullRequestDraft:
                    navService.Navigate<Views.Repositories.PullRequests.ConversationPage>(
                    new FrameNavigationArgs()
                    {
                        Login = ViewModel.Item.Repository.Owner.Login,
                        Name = ViewModel.Item.Repository.Name,
                        Number = ViewModel.Item.Subject.Number,
                    });
                    break;
            }
        }
    }
}
