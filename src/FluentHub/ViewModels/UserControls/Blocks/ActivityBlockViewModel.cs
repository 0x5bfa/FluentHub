using FluentHub.Helpers;
using FluentHub.OctokitEx.Queries.Repository;
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
        private bool isForkEvent;
        public bool IsForkEvent { get => isForkEvent; set => SetProperty(ref isForkEvent, value); }

        private bool isWatchEvent;
        public bool IsWatchEvent { get => isWatchEvent; set => SetProperty(ref isWatchEvent, value); }

        private string avatarUrl;
        public string AvatarUrl { get => avatarUrl; set => SetProperty(ref avatarUrl, value); }

        private string actor;
        public string Actor { get => actor; set => SetProperty(ref actor, value); }

        public Activity FullPayload { get; set; }

        public RepoButtonBlockViewModel RepoButtonBlockViewModel { get; set; } = new();

        public UserButtonBlockViewModel UserButtonBlockViewModel { get; set; } = new();

        public async Task GetPayloadContents()
        {
            if (isForkEvent || isWatchEvent)
            {
                RepoButtonBlockViewModel.DisplayDetails = false;
                RepositoryOverviewQueries queries = new();
                var response = await queries.Get(FullPayload.Repo.Name.Split("/")[0], FullPayload.Repo.Name.Split("/")[1]);
                RepoButtonBlockViewModel.Item = response;
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
