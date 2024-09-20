using FluentHub.App.Extensions;
using FluentHub.App.Models;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.Octokit.Queries.Users;
using Microsoft.UI.Xaml;
using System.IO;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;

namespace FluentHub.App.ViewModels.AppSettings
{
	public class GeneralViewModel : BaseViewModel
	{
		public string Version
		{
			get
			{
				string architecture = Windows.ApplicationModel.Package.Current.Id.Architecture.ToString();

#if DEBUG
				string buildConfiguration = "DEBUG";
#else
				string buildConfiguration = "RELEASE";
#endif

				return $"{App.AppVersion} | {architecture} | {buildConfiguration}";
			}
		}

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

		public ICommand LoadGeneralPageCommand { get; }

		public ICommand CopyVersionCommand { get; }
		public ICommand OpenLogsCommand { get; }

		public GeneralViewModel() : base()
		{
			InitializeDefaultValues();

			LoadGeneralPageCommand = new AsyncRelayCommand(LoadGeneralPageAsync);
			CopyVersionCommand = new RelayCommand(ExecuteCopyVersion);
			OpenLogsCommand = new AsyncRelayCommand(ExecuteOpenLogsAsync);
		}

		private async Task LoadGeneralPageAsync()
		{
			SetTabInformation("Settings", "Settings", "Settings");

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			_currentTaskingMethodName = nameof(LoadGeneralPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync();

				SetTabInformation("Settings", "Settings");
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;

				_logger?.Error(_currentTaskingMethodName, ex);
			}
			finally
			{
				_messenger?.Send(new TaskStateMessaging(IsTaskFaulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
			}
		}

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

		private void ExecuteCopyVersion()
		{
			try
			{
				var data = new DataPackage
				{
					RequestedOperation = DataPackageOperation.Copy
				};

				data.SetText(Version);

				Clipboard.SetContentWithOptions(data, new ClipboardContentOptions() { IsAllowedInHistory = true, IsRoamable = true });
				Clipboard.Flush();
			}
			catch (Exception ex)
			{
				_logger?.Error($"Failed to copy data from copy version command, version: ${Version}", ex);
			}
		}

		private async Task ExecuteOpenLogsAsync()
		{
			string logsFolder = Path.Combine(ApplicationData.Current.LocalFolder.Path, "FluentHub.Logs");
			var result = await Windows.System.Launcher.LaunchFolderPathAsync(logsFolder);
			_logger?.Info("Open logs folder result: {0}", result);
		}

		private void InitializeDefaultValues()
		{
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
		}
	}
}
