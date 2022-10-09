using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.Overview
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
            var service = App.Current.Services.GetRequiredService<INavigationService>();

            switch (args.InvokedItemContainer?.Tag?.ToString().ToLower())
            {
                default:
                case "code":
                    var param = new Models.FrameNavigationArgs()
                    {
                        Login = ViewModel.Repository?.Owner?.Login,
                        Name = ViewModel.Repository?.Name,
                    };

                    if (App.AppSettings.UseDetailsView)
                        service.Navigate<Views.Repositories.Code.Layouts.DetailsLayoutView>(param);
                    else
                        service.Navigate<Views.Repositories.Code.Layouts.TreeLayoutView>(param);
                    break;
                case "issues":
                    service.Navigate(
                        typeof(Views.Repositories.Issues.IssuesPage),
                        new Models.FrameNavigationArgs()
                        {
                            Login = ViewModel.Repository?.Owner?.Login,
                            Name = ViewModel.Repository?.Name,
                        });
                    break;
                case "pullrequests":
                    service.Navigate(
                        typeof(Views.Repositories.PullRequests.PullRequestsPage),
                        new Models.FrameNavigationArgs()
                        {
                            Login = ViewModel.Repository?.Owner?.Login,
                            Name = ViewModel.Repository?.Name,
                        });
                    break;
                case "discussions":
                    service.Navigate(
                        typeof(Views.Repositories.Discussions.DiscussionsPage),
                        new Models.FrameNavigationArgs()
                        {
                            Login = ViewModel.Repository?.Owner?.Login,
                            Name = ViewModel.Repository?.Name,
                        });
                    break;
                case "projects":
                    service.Navigate(
                        typeof(Views.Repositories.Projects.ProjectsPage),
                        new Models.FrameNavigationArgs()
                        {
                            Login = ViewModel.Repository?.Owner?.Login,
                            Name = ViewModel.Repository?.Name,
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
