using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Views.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.ButtonBlocks
{
    public sealed partial class NotificationButtonBlock : UserControl
    {
        #region properties
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(NotificationButtonBlockViewModel),
                  typeof(NotificationButtonBlockViewModel),
                  typeof(NotificationButtonBlock),
                  new PropertyMetadata(null)
                );

        public NotificationButtonBlockViewModel ViewModel
        {
            get => (NotificationButtonBlockViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        #endregion

        public NotificationButtonBlock() => InitializeComponent();

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
