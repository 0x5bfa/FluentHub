// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using FluentHub.App.ViewModels.Repositories.Codes;

namespace FluentHub.App.Views.Repositories.Code
{
    public sealed partial class DetailsLayoutView : LocatablePage
    {
        public DetailsLayoutViewModel ViewModel { get; }

        private readonly INavigationService _navigation;

        public DetailsLayoutView()
            : base(NavigationBarPageKind.Repository, NavigationBarItemKey.Code)
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<DetailsLayoutViewModel>();
            _navigation = App.Current.Services.GetRequiredService<INavigationService>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var command = ViewModel.LoadDetailsViewPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void OnDirListViewDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var item = DirListView.SelectedItem as DetailsLayoutListViewModel;
            var objType = item?.Type;

            string path = ViewModel.ContextViewModel.Path;

            if (string.IsNullOrEmpty(path) is false)
            {
                path += "/";
            }

            path += item.Name;
            List<object> param = new();
            param.Add(objType + "/" + ViewModel.ContextViewModel.BranchName + "/" + path);

            _navigation.Navigate<DetailsLayoutView>(
                new FrameNavigationParameter()
                {
                    UserLogin = ViewModel.Repository.Owner.Login,
                    RepositoryName = ViewModel.Repository.Name,
                    Parameters = param,
                });
        }

        private void OnLatestReleaseClick(object sender, RoutedEventArgs e)
        {
            _navigation.Navigate<Views.Repositories.Releases.ReleasesPage>(
                new FrameNavigationParameter()
                {
                    UserLogin = ViewModel.Repository.Owner.Login,
                    RepositoryName = ViewModel.Repository.Name,
                    Parameters = new() { ViewModel.ContextViewModel },
                });
        }
    }
}
