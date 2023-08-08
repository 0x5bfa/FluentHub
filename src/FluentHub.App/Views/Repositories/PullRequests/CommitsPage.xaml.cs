// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.PullRequests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.PullRequests
{
    public sealed partial class CommitsPage : LocatablePage
    {
        public CommitsViewModel ViewModel { get; }
        private readonly INavigationService _navigation;

        public CommitsPage()
            : base(NavigationPageKind.Repository, NavigationPageKey.PullRequests)
        {
            InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<CommitsViewModel>();
            _navigation = Ioc.Default.GetRequiredService<INavigationService>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationParameter;
            ViewModel.Login = param.PrimaryText;
            ViewModel.Name = param.SecondaryText;
            ViewModel.Number = param.Number;

            var command = ViewModel.LoadRepositoryPullRequestCommitsPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
