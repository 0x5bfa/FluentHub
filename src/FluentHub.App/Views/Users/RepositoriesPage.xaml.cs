// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Users;
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
            var command = ViewModel.LoadUserRepositoriesPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
