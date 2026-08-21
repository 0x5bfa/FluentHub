using FluentHub.Utils;
using FluentHub.Models;
using FluentHub.ViewModels.UserControls.BlockButtons;

namespace FluentHub.ViewModels.UserControls.FeedBlocks
{
	public class ActivityBlockViewModel : ObservableObject
	{
		private readonly IFluentHubGitHubClient _gitHub;

		private Activity _payload = default!;
		public Activity Payload { get => _payload; set => SetProperty(ref _payload, value); }

		private RepoBlockButtonViewModel _repoBlockViewModel = default!;
		public RepoBlockButtonViewModel RepoBlockViewModel { get => _repoBlockViewModel; set => SetProperty(ref _repoBlockViewModel, value); }

		private UserBlockButtonViewModel _userBlockViewModel = default!;
		public UserBlockButtonViewModel UserBlockViewModel { get => _userBlockViewModel; set => SetProperty(ref _userBlockViewModel, value); }

		private IssueBlockButtonViewModel _issueBlockButtonViewModel = default!;
		public IssueBlockButtonViewModel IssueBlockButtonViewModel { get => _issueBlockButtonViewModel; set => SetProperty(ref _issueBlockButtonViewModel, value); }

		private PullBlockButtonViewModel _pullBlockButtonViewModel = default!;
		public PullBlockButtonViewModel PullBlockButtonViewModel { get => _pullBlockButtonViewModel; set => SetProperty(ref _pullBlockButtonViewModel, value); }

		private SingleCommentBlockViewModel _singleCommentBlockViewModel = default!;
		public SingleCommentBlockViewModel SingleCommentBlockViewModel { get => _singleCommentBlockViewModel; set => SetProperty(ref _singleCommentBlockViewModel, value); }

		private SingleCommitBlockViewModel _singleCommitBlockViewModel = default!;
		public SingleCommitBlockViewModel SingleCommitBlockViewModel { get => _singleCommitBlockViewModel; set => SetProperty(ref _singleCommitBlockViewModel, value); }

		private SingleReleaseBlockViewModel _singleReleaseBlockViewModel = default!;
		public SingleReleaseBlockViewModel SingleReleaseBlockViewModel { get => _singleReleaseBlockViewModel; set => SetProperty(ref _singleReleaseBlockViewModel, value); }


		public ActivityBlockViewModel(IFluentHubGitHubClient gitHub)
		{
			_gitHub = gitHub;
		}

		public async Task LoadContentsAsync()
		{
			var repositoryQueries = _gitHub.Repositories.Repositories;
			var userQueries = _gitHub.Users.Users;
			var payload = Payload;

			async Task<FluentHub.Octokit.Contracts.Repository?> LoadRepositoryAsync()
			{
				var owner = payload.Repository?.Owner?.Login;
				var name = payload.Repository?.Name;
				if (string.IsNullOrWhiteSpace(owner) || string.IsNullOrWhiteSpace(name))
					return null;

				return await repositoryQueries.GetAsync(owner, name);
			}

			switch (payload.Type.ToString())
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
						var response = await LoadRepositoryAsync();
						if (response is null)
							break;

						RepoBlockViewModel = new(_gitHub)
						{
							DisplayDetails = true,
							DisplayStarButton = true,
							Repository = response,
						};
						break;
					}
				case "ForkEvent":
					{
						var response = await LoadRepositoryAsync();
						if (response is null)
							break;

						RepoBlockViewModel = new(_gitHub)
						{
							DisplayDetails = true,
							DisplayStarButton = true,
							Repository = response,
						};
						break;
					}
				case "IssueCommentEvent":
					{
						if (payload.PayloadSets?.IssueCommentPayload is null)
							break;

						SingleCommentBlockViewModel = new()
						{
							IssueCommentPayload = payload.PayloadSets.IssueCommentPayload,
						};
					}
					break;
				case "IssueEvent":
					{
						if (payload.PayloadSets?.IssueCommentPayload?.Issue is null)
							break;

						IssueBlockButtonViewModel = new()
						{
							IssueItem = payload.PayloadSets.IssueCommentPayload.Issue,
						};
					}
					break;
				case "PullRequestComment":
					{
					}
					break;
				case "PullRequestEvent":
					{
						if (payload.PayloadSets?.PullRequestEventPayload?.PullRequest is null)
							break;

						PullBlockButtonViewModel = new()
						{
							PullItem = payload.PayloadSets.PullRequestEventPayload.PullRequest,
						};
					}
					break;
				case "PullRequestReviewEvent":
					{
					}
					break;
				case "PushEvent":
					if (payload.PayloadSets?.PushEventPayload is null)
						break;

					SingleCommitBlockViewModel = new()
					{
						PushEventPayload = payload.PayloadSets.PushEventPayload,
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
					if (payload.PayloadSets?.ReleaseEventPayload is null)
						break;

					SingleReleaseBlockViewModel = new()
					{
						ReleaseEventPayload = payload.PayloadSets.ReleaseEventPayload,
					};
					break;
				case "WatchEvent":
					{
						var response = await LoadRepositoryAsync();
						if (response is null)
							break;

						RepoBlockViewModel = new(_gitHub)
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
