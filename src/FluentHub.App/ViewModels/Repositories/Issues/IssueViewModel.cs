using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.Repositories.Issues
{
	public class IssueViewModel : BaseViewModel
	{
		private Issue _issueItem;
		public Issue IssueItem { get => _issueItem; private set => SetProperty(ref _issueItem, value); }

		private readonly ObservableCollection<object> _timelineItems;
		public ReadOnlyObservableCollection<object> TimelineItems { get; set; }

		public IAsyncRelayCommand LoadRepositoryIssuePageCommand { get; }

		public IssueViewModel() : base()
		{
			_timelineItems = new();
			TimelineItems = new(_timelineItems);

			LoadRepositoryIssuePageCommand = new AsyncRelayCommand(LoadRepositoryIssuePageAsync);
		}

		private async Task LoadRepositoryIssuePageAsync()
		{
			SetTabInformation("Issue", "Issue", "Issues");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryIssuePageAsync);

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
			}
			finally
			{
				SetLoadingProgress(false);
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
		}
	}
}
