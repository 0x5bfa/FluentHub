// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Users
{
    public sealed partial class FollowersPage : Page
    {
        private readonly INavigationService _navigationService;

        public FollowersViewModel ViewModel { get; }

        public FollowersPage()
        {
            InitializeComponent();

            _navigationService = Ioc.Default.GetRequiredService<INavigationService>();
            ViewModel = Ioc.Default.GetRequiredService<FollowersViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = (FrameNavigationParameter)e.Parameter;

            ViewModel.Login = parameter.UserLogin
                ?? throw new ArgumentNullException(nameof(parameter.UserLogin), "Login parameter cannot be null in this context.");

            if (parameter.AsViewer)
            {
                ViewModel.DisplayTitle = true;

                var currentTabItem = _navigationService.TabView.SelectedItem;
                currentTabItem.NavigationBar.PageKind = Core.Data.Enums.NavigationPageKind.None;
            }

            var command = ViewModel.LoadUserFollowersPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
