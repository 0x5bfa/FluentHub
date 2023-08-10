using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Repositories.Projects
{
	public class ProjectsViewModel : ObservableObject
	{
		private readonly IMessenger _messenger;
		private readonly ILogger _logger;

		private string _login;
		public string Login { get => _login; set => SetProperty(ref _login, value); }

		private string _name;
		public string Name { get => _name; set => SetProperty(ref _name, value); }

		private Repository _repository;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		private RepositoryOverviewViewModel _repositoryOverviewViewModel;
		public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

		private readonly ObservableCollection<ProjectBlockButtonViewModel> _items;
		public ReadOnlyObservableCollection<ProjectBlockButtonViewModel> Items { get; }

		private Exception _taskException;
		public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

		public IAsyncRelayCommand LoadRepositoryProjectsPageCommand { get; }

		public ProjectsViewModel(IMessenger messenger = null, ILogger logger = null)
		{
			_messenger = messenger;
			_logger = logger;

			_items = new();
			Items = new(_items);

			LoadRepositoryProjectsPageCommand = new AsyncRelayCommand(LoadRepositoryProjectsPageAsync);
		}

		private async Task LoadRepositoryProjectsPageAsync()
		{
			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			bool faulted = false;

			string _currentTaskingMethodName = nameof(LoadRepositoryProjectsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadProjectsPageAsync);
				await LoadProjectsPageAsync(Login, Name);
			}
			catch (Exception ex)
			{
				TaskException = ex;
				faulted = true;

				_logger?.Error(_currentTaskingMethodName, ex);
				throw;
			}
			finally
			{
				SetCurrentTabItem();
				_messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
			}
		}

		private async Task LoadProjectsPageAsync(string owner, string name)
		{
			ProjectQueries queries = new();
			var items = await queries.GetAllAsync(owner, name);

			_items.Clear();
			foreach (var item in items)
			{
				ProjectBlockButtonViewModel viewmodel = new()
				{
					Item = item,
				};

				_items.Add(viewmodel);
			}
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

				SelectedTag = "projects",
			};
		}

		private void SetCurrentTabItem()
		{
			INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

			var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
			currentItem.Header = "Projects";
			currentItem.Description = "Projects";
			currentItem.Icon = new ImageIconSource
			{
				ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Projects.png"))
			};
		}
	}
}
