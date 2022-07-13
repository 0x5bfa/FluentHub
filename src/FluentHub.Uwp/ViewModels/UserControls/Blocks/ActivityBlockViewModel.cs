using FluentHub.Octokit.Authorization;
using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;

namespace FluentHub.Uwp.ViewModels.UserControls.Blocks
{
    public class ActivityBlockViewModel : ObservableObject
    {
        public ActivityBlockViewModel()
        {
        }

        private Activity _payload;
        public Activity Payload { get => _payload; set => SetProperty(ref _payload, value); }

        private RepoButtonBlockViewModel _repoBlockViewModel;
        public RepoButtonBlockViewModel RepoBlockViewModel { get => _repoBlockViewModel; set => SetProperty(ref _repoBlockViewModel, value); }

        private UserButtonBlockViewModel _userBlockViewModel;
        public UserButtonBlockViewModel UserBlockViewModel { get => _userBlockViewModel; set => SetProperty(ref _userBlockViewModel, value); }

        private SingleCommentBlockViewModel _singleCommentBlockViewModel;
        public SingleCommentBlockViewModel SingleCommentBlockViewModel { get => _singleCommentBlockViewModel; set => SetProperty(ref _singleCommentBlockViewModel, value); }

        private SingleCommitBlockViewModel _singleCommitBlockViewModel;
        public SingleCommitBlockViewModel SingleCommitBlockViewModel { get => _singleCommitBlockViewModel; set => SetProperty(ref _singleCommitBlockViewModel, value); }

        private SingleReleaseBlockViewModel _singleReleaseBlockViewModel;
        public SingleReleaseBlockViewModel SingleReleaseBlockViewModel { get => _singleReleaseBlockViewModel; set => SetProperty(ref _singleReleaseBlockViewModel, value); }

        //private CommitActivityBlockViewModel commitBlockViewModel;
        //public CommitActivityBlockViewModel CommitBlockViewModel { get => commitBlockViewModel; set => SetProperty(ref commitBlockViewModel, value); }

        public async Task LoadContentsAsync()
        {
            Octokit.Queries.Repositories.RepositoryQueries repositoryQueries = new();
            Octokit.Queries.Users.UserQueries userQueries = new();

            switch (Payload.PayloadType)
            {
                case "WatchEvent":
                case "ForkEvent":
                case "CreateEvent":
                case "DeleteEvent":
                    var response = await repositoryQueries.GetAsync(Payload.Repository.Owner.Login, Payload.Repository.Name);

                    RepoBlockViewModel = new()
                    {
                        DisplayDetails = true,
                        DisplayStarButton = true,
                        Item = response,
                    };
                    break;
                case "IssueCommentEvent":
                    SingleCommentBlockViewModel = new()
                    {
                        IssueCommentPayload = Payload.IssueCommentPayload,
                    };
                    break;
                case "PushEvent":
                    SingleCommitBlockViewModel = new()
                    {
                        PushEventPayload = Payload.PushEventPayload,
                    };
                    break;
                case "ReleaseEvent":
                    SingleReleaseBlockViewModel = new()
                    {
                        ReleaseEventPayload = Payload.ReleaseEventPayload,
                    };
                    break;
            }
        }
    }
}
