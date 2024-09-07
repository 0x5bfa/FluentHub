using FluentHub.App.ViewModels.UserControls.BlockButtons;

namespace FluentHub.App.ViewModels.UserControls.FeedBlocks
{
	public class ActivityBlockViewModel : ObservableObject
	{
		private Activity _payload;
		public Activity Payload { get => _payload; set => SetProperty(ref _payload, value); }

		private RepoBlockButtonViewModel _repoBlockViewModel;
		public RepoBlockButtonViewModel RepoBlockViewModel { get => _repoBlockViewModel; set => SetProperty(ref _repoBlockViewModel, value); }

		private UserBlockButtonViewModel _userBlockViewModel;
		public UserBlockButtonViewModel UserBlockViewModel { get => _userBlockViewModel; set => SetProperty(ref _userBlockViewModel, value); }

		private IssueBlockButtonViewModel _issueBlockButtonViewModel;
		public IssueBlockButtonViewModel IssueBlockButtonViewModel { get => _issueBlockButtonViewModel; set => SetProperty(ref _issueBlockButtonViewModel, value); }

		private PullBlockButtonViewModel _pullBlockButtonViewModel;
		public PullBlockButtonViewModel PullBlockButtonViewModel { get => _pullBlockButtonViewModel; set => SetProperty(ref _pullBlockButtonViewModel, value); }

		private SingleCommentBlockViewModel _singleCommentBlockViewModel;
		public SingleCommentBlockViewModel SingleCommentBlockViewModel { get => _singleCommentBlockViewModel; set => SetProperty(ref _singleCommentBlockViewModel, value); }

		private SingleCommitBlockViewModel _singleCommitBlockViewModel;
		public SingleCommitBlockViewModel SingleCommitBlockViewModel { get => _singleCommitBlockViewModel; set => SetProperty(ref _singleCommitBlockViewModel, value); }

		private SingleReleaseBlockViewModel _singleReleaseBlockViewModel;
		public SingleReleaseBlockViewModel SingleReleaseBlockViewModel { get => _singleReleaseBlockViewModel; set => SetProperty(ref _singleReleaseBlockViewModel, value); }


		public ActivityBlockViewModel()
		{
		}

		public async Task LoadContentsAsync()
		{
			Octokit.Queries.Repositories.RepositoryQueries repositoryQueries = new();
			Octokit.Queries.Users.UserQueries userQueries = new();

			switch (Payload.Type.ToString())
			{
				case "CheckRunEvent":
					{
					}
					break;
				case "CheckSuiteEvent":
					{
					}
					break;
				case "CommitComment":
					{
					}
					break;
				case "CreateEvent":
					{
					}
					break;
				case "DeleteEvent":
					{
						var response = await repositoryQueries.GetAsync(Payload.Repository.Owner.Login, Payload.Repository.Name);

						RepoBlockViewModel = new()
						{
							DisplayDetails = true,
							DisplayStarButton = true,
							Repository = response,
						};
						break;
					}
				case "ForkEvent":
					{
						var response = await repositoryQueries.GetAsync(Payload.Repository.Owner.Login, Payload.Repository.Name);

						RepoBlockViewModel = new()
						{
							DisplayDetails = true,
							DisplayStarButton = true,
							Repository = response,
						};
						break;
					}
				case "IssueCommentEvent":
					{
						SingleCommentBlockViewModel = new()
						{
							IssueCommentPayload = Payload.PayloadSets.IssueCommentPayload,
						};
					}
					break;
				case "IssueEvent":
					{
						IssueBlockButtonViewModel = new()
						{
							IssueItem = Payload.PayloadSets.IssueCommentPayload.Issue,
						};
					}
					break;
				case "PullRequestComment":
					{
					}
					break;
				case "PullRequestEvent":
					{
						PullBlockButtonViewModel = new()
						{
							PullItem = Payload.PayloadSets.PullRequestEventPayload.PullRequest,
						};
					}
					break;
				case "PullRequestReviewEvent":
					{
					}
					break;
				case "PushEvent":
					SingleCommitBlockViewModel = new()
					{
						PushEventPayload = Payload.PayloadSets.PushEventPayload,
					};
					break;
				case "PushWebhookCommit":
					{
					}
					break;
				case "PushWebhookCommitter":
					{
					}
					break;
				case "PushWebhook":
					{
					}
					break;
				case "ReleaseEvent":
					SingleReleaseBlockViewModel = new()
					{
						ReleaseEventPayload = Payload.PayloadSets.ReleaseEventPayload,
					};
					break;
				case "WatchEvent":
					{
						var response = await repositoryQueries.GetAsync(Payload.Repository.Owner.Login, Payload.Repository.Name);

						RepoBlockViewModel = new()
						{
							DisplayDetails = true,
							DisplayStarButton = true,
							Repository = response,
						};
						break;
					}
				case "StatusEvent":
					{
					}
					break;
			}
		}
	}
}
