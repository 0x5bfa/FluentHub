// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;

namespace FluentHub.App.ViewModels.UserControls.BlockButtons
{
    public class RepoBlockButtonViewModel : ObservableObject
    {
        public RepoBlockButtonViewModel()
        {
            GoRepositoryCommand = new RelayCommand(GoRepository);
        }

        #region Fields and Properties
        private Repository _item;
        public Repository Repository { get => _item; set => SetProperty(ref _item, value); }

        private bool _displayDetails;
        public bool DisplayDetails { get => _displayDetails; set => SetProperty(ref _displayDetails, value); }

        private bool _displayStarButton;
        public bool DisplayStarButton { get => _displayStarButton; set => SetProperty(ref _displayStarButton, value); }

        public ICommand GoRepositoryCommand { get; private set; }
        #endregion

        private void GoRepository()
        {
            var _navigation = App.Current.Services.GetRequiredService<INavigationService>();
            var parameter = _navigation.TabView.SelectedItem.NavigationBar.Parameter;

            parameter.UserLogin = Repository.Owner.Login;
            parameter.RepositoryName = Repository.Name;

            if (App.AppSettings.UseDetailsView)
                _navigation.Navigate<Views.Repositories.Code.DetailsLayoutView>();
            else
                _navigation.Navigate<Views.Repositories.Code.TreeLayoutView>();
        }
    }
}
