using FluentHub.Helpers;
using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class ActivityBlockViewModel : INotifyPropertyChanged
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

        public async Task GetPayloadContents()
        {
            if (Payload.IsForkEvent)
            {
                RepoBlockViewModel = new();
                RepoBlockViewModel.DisplayDetails = false;
                RepoBlockViewModel.DisplayStarButton = true;

                RepositoryQueries queries = new();
                var repo = await queries.GetOverview(Payload.Repository.Name.Split("/")[1], Payload.Repository.Name.Split("/")[0]);
                RepoBlockViewModel.Item = repo;
            }
            else if (Payload.IsWatchEvent)
            {
                RepoBlockViewModel = new();
                RepoBlockViewModel.DisplayDetails = false;
                RepoBlockViewModel.DisplayStarButton = true;

                RepositoryQueries queries = new();
                var repo = await queries.GetOverview(Payload.Repository.Name.Split("/")[1], Payload.Repository.Name.Split("/")[0]);
                RepoBlockViewModel.Item = repo;
            }
            else if (Payload.IsPushEvent)
            {
                CommitBlockViewModel = new();
                CommitBlockViewModel.Ssh = Payload.PushEventPayload.Head;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
