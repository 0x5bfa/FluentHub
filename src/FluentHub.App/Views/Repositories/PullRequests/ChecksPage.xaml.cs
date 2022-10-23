using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Services.Navigation;
using FluentHub.App.ViewModels.Repositories.PullRequests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.Views.Repositories.PullRequests
{
    public sealed partial class ChecksPage : Page
    {
        public ChecksPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ChecksViewModel>();
            _navigation = provider.GetRequiredService<INavigationService>();
        }

        public ChecksViewModel ViewModel { get; }
        private readonly INavigationService _navigation;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationArgs;
            ViewModel.Login = param.Login;
            ViewModel.Name = param.Name;
            ViewModel.Number = param.Number;

            var command = ViewModel.LoadRepositoryPullRequestChecksPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void OnCheckRunItemButtonClick(object sender, RoutedEventArgs e)
        {
            var target = sender as Button;
            var checkRunItem = target.Tag as CheckRun;

            ViewModel.SelectedCheckRun = checkRunItem;
        }
    }
}
