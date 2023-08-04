// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.PullRequests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.App.Data.Parameters;

namespace FluentHub.App.Views.Repositories.PullRequests
{
    public sealed partial class ChecksPage : LocatablePage
    {
        public ChecksViewModel ViewModel { get; }

        private readonly INavigationService _navigation;

        public ChecksPage()
            : base(NavigationPageKind.Repository, NavigationPageKey.PullRequests)
        {
            InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<ChecksViewModel>();
            _navigation = Ioc.Default.GetRequiredService<INavigationService>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationParameter;
            ViewModel.Login = param.UserLogin;
            ViewModel.Name = param.RepositoryName;
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
