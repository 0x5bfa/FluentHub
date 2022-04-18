using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class ActivityBlockViewModel : ObservableObject
    {
        private Octokit.Models.Activity payload;
        public Octokit.Models.Activity Payload { get => payload; set => SetProperty(ref payload, value); }

        private string updatedAtHumanized;
        public string UpdatedAtHumanized { get => updatedAtHumanized; set => SetProperty(ref updatedAtHumanized, value); }

        private RepoButtonBlockViewModel repoBlockViewModel;
        public RepoButtonBlockViewModel RepoBlockViewModel { get => repoBlockViewModel; set => SetProperty(ref repoBlockViewModel, value); }

        private UserButtonBlockViewModel userBlockViewModel;
        public UserButtonBlockViewModel UserBlockViewModel { get => userBlockViewModel; set => SetProperty(ref userBlockViewModel, value); }

        private CommitActivityBlockViewModel commitBlockViewModel;
        public CommitActivityBlockViewModel CommitBlockViewModel { get => commitBlockViewModel; set => SetProperty(ref commitBlockViewModel, value); }

        public async Task GetPayloadContentsAsync()
        {
            if (Payload.IsForkEvent)
            {
                RepoBlockViewModel = new();
                RepoBlockViewModel.DisplayDetails = false;
                RepoBlockViewModel.DisplayStarButton = true;

                RepositoryQueries queries = new();
                var repo = await queries.Get(Payload.Repository.Name.Split("/")[0], Payload.Repository.Name.Split("/")[1]);
                RepoBlockViewModel.Item = repo;
            }
            else if (Payload.IsWatchEvent)
            {
                RepoBlockViewModel = new();
                RepoBlockViewModel.DisplayDetails = false;
                RepoBlockViewModel.DisplayStarButton = true;

                RepositoryQueries queries = new();
                var repo = await queries.Get(Payload.Repository.Name.Split("/")[0], Payload.Repository.Name.Split("/")[1]);
                RepoBlockViewModel.Item = repo;
            }
            else if (Payload.IsPushEvent)
            {
                CommitBlockViewModel = new();
                CommitBlockViewModel.Ssh = Payload.PushEventPayload.Head;
            }
        }
    }
}