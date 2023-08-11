using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Repositories.Issues
{
	public class IssueViewModel : BaseViewModel
	{
		private Repository _repository;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		private RepositoryOverviewViewModel _repositoryOverviewViewModel;
		public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

		private Issue _issueItem;
		public Issue IssueItem { get => _issueItem; private set => SetProperty(ref _issueItem, value); }

		private readonly ObservableCollection<object> _timelineItems;
		public ReadOnlyObservableCollection<object> TimelineItems { get; set; }

		public IAsyncRelayCommand LoadRepositoryIssuePageCommand { get; }

		public IssueViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText;
			Name = parameter.SecondaryText;
			Number = parameter.Number;

			_timelineItems = new();
			TimelineItems = new(_timelineItems);

			LoadRepositoryIssuePageCommand = new AsyncRelayCommand(LoadRepositoryIssuePageAsync);
		}

		private async Task LoadRepositoryIssuePageAsync()
		{
			SetTabInformation("Issue", "Issue", "Issues");

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

			string _currentTaskingMethodName = nameof(LoadRepositoryIssuePageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryOneIssueAsync);
				await LoadRepositoryOneIssueAsync(Login, Name);

				SetTabInformation($"{IssueItem.Title}", $"{IssueItem.Title}");
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

		private async Task LoadRepositoryOneIssueAsync(string owner, string name)
		{
			IssueQueries issueQueries = new();
			IssueEventQueries queries = new();
			_timelineItems.Clear();

			IssueItem = await issueQueries.GetAsync(owner, name, Number);

			var bodyComment = await issueQueries.GetBodyAsync(owner, name, Number);
			_timelineItems.Add(bodyComment);

			var issueEvents = await queries.GetAllAsync(owner, name, Number);
			foreach (var item in issueEvents)
				_timelineItems.Add(item);
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

				SelectedTag = "issues",
			};
		}

		private void SetCurrentTabItem()
		{
			INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

			var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
			currentItem.Header = $"{IssueItem?.Title} · #{IssueItem?.Number}";
			currentItem.Description = currentItem.Header;
			currentItem.Icon = new ImageIconSource
			{
				ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Issues.png"))
			};
		}
	}
}
