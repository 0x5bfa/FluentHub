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
    public sealed partial class DiscussionsPage : Page
    {
        private readonly INavigationService _navigationService;

        public DiscussionsViewModel ViewModel { get; }

        public DiscussionsPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            _navigationService = provider.GetRequiredService<INavigationService>();
            ViewModel = provider.GetRequiredService<DiscussionsViewModel>();
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
                currentTabItem.NavigationBar.PageKind = NavigationBarPageKind.None;
            }

            var command = ViewModel.LoadUserDiscussionsPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
