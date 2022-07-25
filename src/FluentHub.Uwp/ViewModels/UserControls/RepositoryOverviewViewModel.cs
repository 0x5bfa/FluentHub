using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.Labels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FluentHub.Uwp.ViewModels.UserControls
{
    public class RepositoryOverviewViewModel : ObservableObject
    {
        public RepositoryOverviewViewModel()
        {
            RepositoryVisibilityLabel = new()
            {
                Color = "#64000000"
            };

            GoOwnerProfileCommand = new RelayCommand(GoOwnerProfile);
        }

        #region Fields and Properties
        private string _repositoryOwnerLogin;
        public string RepositoryOwnerLogin { get => _repositoryOwnerLogin; set => SetProperty(ref _repositoryOwnerLogin, value); }

        private string _repositoryName;
        public string RepositoryName { get => _repositoryName; set => SetProperty(ref _repositoryName, value); }

        private string _selectedTag;
        public string SelectedTag { get => _selectedTag; set => SetProperty(ref _selectedTag, value); }

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private LabelControlViewModel _repositoryVisibilityLabel;
        public LabelControlViewModel RepositoryVisibilityLabel { get => _repositoryVisibilityLabel; set => SetProperty(ref _repositoryVisibilityLabel, value); }

        private string _viewerSubscriptionState;
        public string ViewerSubscriptionState { get => _viewerSubscriptionState; set => SetProperty(ref _viewerSubscriptionState, value); }

        public ICommand GoOwnerProfileCommand { get; private set; }
        #endregion

        #region Command methods
        private void GoOwnerProfile()
        {
            var service = App.Current.Services.GetRequiredService<INavigationService>();

            if (Repository.IsInOrganization)
            {
                service.Navigate<Views.Organizations.ProfilePage>(Repository.Owner.Login);
            }
            else
            {
                service.Navigate<Views.Users.ProfilePage>($"{App.DefaultGitHubDomain}/{Repository.Owner.Login}");
            }
        }
        #endregion
    }
}
