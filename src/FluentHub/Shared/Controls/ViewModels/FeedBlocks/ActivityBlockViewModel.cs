using FluentHub.Utils;
using FluentHub.Models;
using FluentHub.Shared.Controls.ViewModels.BlockButtons;

namespace FluentHub.Shared.Controls.ViewModels.FeedBlocks
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

			async Task<FluentHub.Core.Application.Models.Repository?> LoadRepositoryAsync()
			{
				var owner = payload.Repository?.Owner?.Login;
				var name = payload.Repository?.Name;
				if (string.IsNullOrWhiteSpace(owner) || string.IsNullOrWhiteSpace(name))
					return null;

				return await repositoryQueries.GetAsync(owner, name);
			}

			switch (payload.Type)
			{
				case ActivityKind.CheckRunEvent:
					{
					}
					break;
				case ActivityKind.CheckSuiteEvent:
					{
					}
					break;
				case ActivityKind.CommitComment:
					{
					}
					break;
				case ActivityKind.CreateEvent:
					{
					}
					break;
				case ActivityKind.DeleteEvent:
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
				case ActivityKind.ForkEvent:
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
				case ActivityKind.IssueCommentEvent:
					{
						if (payload.Details.IssueCommentEvent is null)
							break;

						SingleCommentBlockViewModel = new()
						{
							Details = payload.Details.IssueCommentEvent,
						};
					}
					break;
				case ActivityKind.IssueEvent:
					{
						if (payload.Details.IssueEvent?.Issue is null)
							break;

						IssueBlockButtonViewModel = new()
						{
							IssueItem = payload.Details.IssueEvent.Issue,
						};
					}
					break;
				case ActivityKind.PullRequestComment:
					{
					}
					break;
				case ActivityKind.PullRequestEvent:
					{
						if (payload.Details.PullRequestEvent?.PullRequest is null)
							break;

						PullBlockButtonViewModel = new()
						{
							PullItem = payload.Details.PullRequestEvent.PullRequest,
						};
					}
					break;
				case ActivityKind.PullRequestReviewEvent:
					{
					}
					break;
				case ActivityKind.PushEvent:
					if (payload.Details.PushEvent is null)
						break;

					SingleCommitBlockViewModel = new()
					{
						Details = payload.Details.PushEvent,
					};
					break;
				case ActivityKind.ReleaseEvent:
					if (payload.Details.ReleaseEvent is null)
						break;

					SingleReleaseBlockViewModel = new()
					{
						Details = payload.Details.ReleaseEvent,
					};
					break;
				case ActivityKind.WatchEvent:
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
				case ActivityKind.StatusEvent:
					{
					}
					break;
				case ActivityKind.Unknown:
				default:
					break;
			}
		}
	}
}
