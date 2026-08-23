using FluentHub.Core.Queries.Users;
using FluentHub.Core.Caching;
using FluentHub.Extensions;
using FluentHub.Models;
using FluentHub.ViewModels.UserControls.Overview;
using Microsoft.UI.Xaml;
using System.IO;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using FluentHub.Core.Contracts;

namespace FluentHub.ViewModels.AppSettings
{
	public class GeneralViewModel : BaseViewModel
	{
		private readonly ICacheService _cache;

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

		public ReadOnlyCollection<DefaultLanguageModel> DefaultLanguages { get; private set; } = default!;

		public ReadOnlyCollection<string> Themes { get; set; } = default!;

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

		private string _cacheSizeText = "GitHub images and data";
		public string CacheSizeText { get => _cacheSizeText; set => SetProperty(ref _cacheSizeText, value); }

		private AppSettingsOverviewViewModel _appSettingsOverviewViewModel = default!;
		public AppSettingsOverviewViewModel AppSettingsOverviewViewModel { get => _appSettingsOverviewViewModel; set => SetProperty(ref _appSettingsOverviewViewModel, value); }

		public ICommand LoadGeneralPageCommand { get; }

		public ICommand CopyVersionCommand { get; }
		public ICommand OpenLogsCommand { get; }
		public ICommand ClearCacheCommand { get; }

		public GeneralViewModel(IFluentHubGitHubClient gitHub, ICacheService cache) : base(gitHub)
		{
			_cache = cache;
			InitializeDefaultValues();

			LoadGeneralPageCommand = new AsyncRelayCommand(LoadGeneralPageAsync);
			CopyVersionCommand = new RelayCommand(ExecuteCopyVersion);
			OpenLogsCommand = new AsyncRelayCommand(ExecuteOpenLogsAsync);
			ClearCacheCommand = new AsyncRelayCommand(ExecuteClearCacheAsync);
		}

		private async Task LoadGeneralPageAsync()
		{
			SetTabInformation("Settings", "Settings", "Settings");

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			_currentTaskingMethodName = nameof(LoadGeneralPageAsync);

			try
			{
				await RefreshCacheSizeAsync();

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
				var queries = _gitHub.Users.Users;
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

		private async Task ExecuteClearCacheAsync()
		{
			await _cache.ClearAsync();
			await RefreshCacheSizeAsync();
			_logger?.Info("GitHub image and data cache cleared");
		}

		private async Task RefreshCacheSizeAsync()
		{
			var size = await _cache.GetSizeAsync();
			CacheSizeText = $"GitHub images and data · {HumanReadableFormatter.FormatFileSize(size)}";
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
