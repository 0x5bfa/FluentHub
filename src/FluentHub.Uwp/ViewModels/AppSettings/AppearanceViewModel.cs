using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.BlockButtons;
using FluentHub.Uwp.ViewModels.UserControls.Overview;
using FluentHub.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Uwp;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.AppSettings
{
    public class AppearanceViewModel : ObservableObject
    {
        #region Constructor
        public AppearanceViewModel(ILogger logger = null)
        {
            _logger = logger;

            _selectedThemeIndex = (int)Enum.Parse<ElementTheme>(ThemeHelper.RootTheme.ToString());
            _selectedLanguageIndex = App.Settings.DefaultLanguages.IndexOf(App.Settings.DefaultLanguage);
            _showRestartMessage = false;

            Themes = new List<string>()
            {
                "ThemeAuto".GetLocalized(),
                "ThemeLight".GetLocalized(),
                "ThemeDark".GetLocalized(),
            }
            .AsReadOnly();

            DefaultLanguages = App.Settings.DefaultLanguages.ToList().AsReadOnly();

            LoadUserCommand = new AsyncRelayCommand(LoadUserAsync);

            SetCurrentTabItem();
        }
        #endregion

        #region Fields and Properties
        private readonly ILogger _logger;

        public ReadOnlyCollection<DefaultLanguageModel> DefaultLanguages { get; private set; }
        public ReadOnlyCollection<string> Themes { get; set; }

        private User _user;
        public User User { get => _user; set => SetProperty(ref _user, value); }

        private int _selectedThemeIndex;
        public int SelectedThemeIndex
        {
            get => _selectedThemeIndex;
            set
            {
                if (SetProperty(ref _selectedThemeIndex, value))
                {
                    ThemeHelper.RootTheme = (ElementTheme)value;
                    _logger?.Info("Theme changed to {0}", ThemeHelper.RootTheme);
                }
            }
        }

        private int _selectedLanguageIndex;
        public int SelectedLanguageIndex
        {
            get => _selectedLanguageIndex;
            set
            {
                if (SetProperty(ref _selectedLanguageIndex, value))
                {
                    App.Settings.DefaultLanguage = DefaultLanguages[value];
                    _logger?.Info("Language changed to {0}", App.Settings.DefaultLanguage);

                    ShowRestartMessage = App.Settings.CurrentLanguage.ID != DefaultLanguages[value].ID;
                }
            }
        }

        private bool _showRestartMessage;
        public bool ShowRestartMessage { get => _showRestartMessage; set => SetProperty(ref _showRestartMessage, value); }

        private AppSettingsOverviewViewModel _appSettingsOverviewViewModel;
        public AppSettingsOverviewViewModel AppSettingsOverviewViewModel { get => _appSettingsOverviewViewModel; set => SetProperty(ref _appSettingsOverviewViewModel, value); }

        public IAsyncRelayCommand LoadUserCommand { get; }
        #endregion

        private async Task LoadUserAsync()
        {
            AppSettingsOverviewViewModel = new()
            {
                SelectedTag = "appearance",
            };

            if (AppSettingsOverviewViewModel.StoredUser is null)
            {
                UserQueries queries = new();
                var response = await queries.GetAsync(App.Settings.SignedInUserName);

                User = response;

                AppSettingsOverviewViewModel.StoredUser = User;
                AppSettingsOverviewViewModel.User = User;
            }
            else
            {
                AppSettingsOverviewViewModel.User = AppSettingsOverviewViewModel.StoredUser;
            }
        }

        private void SetCurrentTabItem()
        {
            var provider = App.Current.Services;
            INavigationService navigationService = provider.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Appearance";
            currentItem.Description = "Appearance";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Appearance.png"))
            };
        }
    }
}
