// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Users
{
    public sealed partial class OverviewPage : LocatablePage
    {
        public OverviewViewModel ViewModel { get; }

        public OverviewPage()
            : base(NavigationBarPageKind.User, NavigationBarItemKey.Overview)
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<OverviewViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = (FrameNavigationParameter)e.Parameter;

            ViewModel.Login = param.UserLogin;

            ViewModel.ContextViewModel = new()
            {
                Repository = new()
                {
                    Name = ViewModel.Login,
                    Owner = new RepositoryOwner()
                    {
                        Login = ViewModel.Login,
                    },
                }
            };

            var command = ViewModel.LoadUserOverviewCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
