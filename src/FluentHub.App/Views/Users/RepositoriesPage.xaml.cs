// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Services;
using FluentHub.App.ViewModels.Users;
using FluentHub.Core.Data.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Users
{
    public sealed partial class RepositoriesPage : LocatablePage
    {
        private readonly INavigationService _navigationService;

        public RepositoriesViewModel ViewModel { get; }

        public RepositoriesPage()
            : base(NavigationBarPageKind.User, NavigationBarItemKey.Repositories)
        {
            InitializeComponent();

            var provider = App.Current.Services;
            _navigationService = provider.GetRequiredService<INavigationService>();
            ViewModel = provider.GetRequiredService<RepositoriesViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = (Models.FrameNavigationParameter)e.Parameter;

            ViewModel.Login = parameter.Login
                ?? throw new ArgumentNullException(nameof(parameter.Login), "Login parameter cannot be null in this context.");

            if (parameter.AsViewer)
            {
                ViewModel.DisplayTitle = true;

                var currentTabItem = _navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
                currentTabItem.PageKind = Core.Data.Enums.NavigationBarPageKind.None;
            }

            var command = ViewModel.LoadUserRepositoriesPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
