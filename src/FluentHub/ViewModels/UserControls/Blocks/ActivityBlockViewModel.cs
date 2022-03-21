using FluentHub.Helpers;
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
        public Activity FullPayload { get; set; }

        private bool isForkEvent;
        public bool IsForkEvent { get => isForkEvent; set => SetProperty(ref isForkEvent, value); }

        private bool isWatchEvent;
        public bool IsWatchEvent { get => isWatchEvent; set => SetProperty(ref isWatchEvent, value); }

        private bool isPushEvent;
        public bool IsPushEvent { get => isPushEvent; set => SetProperty(ref isPushEvent, value); }

        private string updatedAtHumanized;
        public string UpdatedAtHumanized { get => updatedAtHumanized; set => SetProperty(ref updatedAtHumanized, value); }

        public RepoButtonBlockViewModel RepoButtonBlockViewModel { get; set; } = new();

        public UserButtonBlockViewModel UserButtonBlockViewModel { get; set; } = new();

        public async Task GetPayloadContents()
        {
            if (isForkEvent || isWatchEvent)
            {
                RepoButtonBlockViewModel.DisplayDetails = false;
                RepositoryQueries queries = new();
                var response = await queries.GetOverview(FullPayload.Repo.Name.Split("/")[0], FullPayload.Repo.Name.Split("/")[1]);
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
