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
    public sealed partial class CommitPage : LocatablePage
    {
        public CommitViewModel ViewModel { get; }

        private readonly INavigationService _navigation;

        public CommitPage()
            : base(NavigationBarPageKind.Repository, NavigationBarItemKey.PullRequests)
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<CommitViewModel>();
            _navigation = App.Current.Services.GetRequiredService<INavigationService>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationParameter;
            ViewModel.Login = param.UserLogin;
            ViewModel.Name = param.RepositoryName;
            ViewModel.Number = param.Number;
            ViewModel.CommitItem = param.Parameters.ElementAtOrDefault(0) as Commit;

            var command = ViewModel.LoadRepositoryPullRequestCommitPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
