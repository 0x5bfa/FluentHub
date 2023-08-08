using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Extensions;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Repositories.Releases
{
	public class ReleaseViewModel : ObservableObject
	{
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;

		private string _login;
		public string Login { get => _login; set => SetProperty(ref _login, value); }

		private string _name;
		public string Name { get => _name; set => SetProperty(ref _name, value); }

		private Repository _repository;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		private RepoContextViewModel contextViewModel;
		public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

		private RepositoryOverviewViewModel _repositoryOverviewViewModel;
		public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

		private string _tagName;
		public string TagName{ get => _tagName; set => SetProperty(ref _tagName, value); }

		private Release _singleRelease;
		public Release SingleRelease { get => _singleRelease; set => SetProperty(ref _singleRelease, value); }

		public WebView2 ReleaseDescriptionWebView2;

		private bool _failedToLoadWebView2Content;
		public bool FailedToLoadWebView2Content { get => _failedToLoadWebView2Content; set => SetProperty(ref _failedToLoadWebView2Content, value); }

		private Exception _taskException;
		public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

		public IAsyncRelayCommand LoadRepositoryReleasePageCommand { get; }

		public ReleaseViewModel(IMessenger messenger = null, ILogger logger = null)
		{
			_messenger = messenger;
			_logger = logger;

			LoadRepositoryReleasePageCommand = new AsyncRelayCommand(LoadRepositoryReleasePageAsync);
		}

		private async Task LoadRepositoryReleasePageAsync()
		{
			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			bool faulted = false;

			string _currentTaskingMethodName = nameof(LoadRepositoryReleasePageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositorySingleReleaseAsync);
				await LoadRepositorySingleReleaseAsync(Login, Name, TagName);
			}
			catch (Exception ex)
			{
				TaskException = ex;

				_logger?.Error(_currentTaskingMethodName, ex);
				_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsFaulted));
				throw;
			}
			finally
			{
				SetCurrentTabItem();
				_messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
			}
		}

		private async Task LoadRepositorySingleReleaseAsync(string login, string name, string tagName)
		{
			 var queries = new ReleaseQueries();
			 var response = await queries.GetAsync(login, name, tagName);

			 SingleRelease = response;
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);

			RepositoryOverviewViewModel = new()
			{
				Repository = Repository,
				RepositoryName = Repository.Name,
				RepositoryOwnerLogin = Repository.Owner.Login,
				ViewerSubscriptionState = Repository.ViewerSubscription?.Humanize(),

				SelectedTag = "code",
			};
		}

		private void SetCurrentTabItem()
		{
			INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

			var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
			currentItem.Header = $"Release · {Login}/{Name}";
			currentItem.Description = $"Release · {Login}/{Name}";
			currentItem.Icon = new ImageIconSource
			{
				ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
			};
		}
	}
}
