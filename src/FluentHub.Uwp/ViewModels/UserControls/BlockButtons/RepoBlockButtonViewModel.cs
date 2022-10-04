using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;

namespace FluentHub.Uwp.ViewModels.UserControls.BlockButtons
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

        #region Command methods
        private void GoRepository()
        {
            var _navigation = App.Current.Services.GetRequiredService<INavigationService>();

            var parameter = new FrameNavigationArgs()
            {
                Login = Repository.Owner.Login,
                Name = Repository.Name,
            };

            if (App.Settings.UseDetailsView)
                _navigation.Navigate<Views.Repositories.Code.Layouts.DetailsLayoutView>(parameter);
            else
                _navigation.Navigate<Views.Repositories.Code.Layouts.TreeLayoutView>(parameter);
        }
        #endregion
    }
}
