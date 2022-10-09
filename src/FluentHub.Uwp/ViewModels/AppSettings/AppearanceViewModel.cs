using CommunityToolkit.WinUI.UI;
using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Extensions;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
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

            _selectedThemeIndex = (int)Enum.Parse<ElementTheme>(ThemeHelpers.RootTheme.ToString());
            _selectedLanguageIndex = App.AppSettings.DefaultLanguages.IndexOf(App.AppSettings.DefaultLanguage);
            _showRestartMessage = false;

            Themes = new List<string>()
            {
                "ThemeAuto".GetLocalizedResource(),
                "ThemeLight".GetLocalizedResource(),
                "ThemeDark".GetLocalizedResource(),
            }
            .AsReadOnly();

            DefaultLanguages = App.AppSettings.DefaultLanguages.ToList().AsReadOnly();

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
                    ThemeHelpers.RootTheme = (ElementTheme)value;
                    _logger?.Info("Theme changed to {0}", ThemeHelpers.RootTheme);
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
                    App.AppSettings.DefaultLanguage = DefaultLanguages[value];
                    _logger?.Info("Language changed to {0}", App.AppSettings.DefaultLanguage);

                    ShowRestartMessage = App.AppSettings.CurrentLanguage.ID != DefaultLanguages[value].ID;
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
                var response = await queries.GetAsync(App.AppSettings.SignedInUserName);

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
