using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;

namespace FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks
{
    public class RepoButtonBlockViewModel : ObservableObject
    {
        public RepoButtonBlockViewModel()
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
            var service = App.Current.Services.GetRequiredService<INavigationService>();
            service.Navigate<Views.Repositories.Codes.CodePage>(
                new FrameNavigationArgs()
                {
                    Login = Repository.Owner.Login,
                    Name = Repository.Name,
                });
        }
        #endregion
    }
}
