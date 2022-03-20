using FluentHub.OctokitEx.Queries;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Users
{
    public class OverviewViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<RepoButtonBlockViewModel> PinnedRepos { get; private set; } = new();

        private long userSpecialReadmeRepoId;
        public long UserSpecialReadmeRepoId { get => userSpecialReadmeRepoId; set => SetProperty(ref userSpecialReadmeRepoId, value); }

        private bool hasUserSpecialReadmeRepoId;
        public bool HasUserSpecialReadmeRepoId { get => hasUserSpecialReadmeRepoId; set => SetProperty(ref hasUserSpecialReadmeRepoId, value); }

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public async Task GetPinnedRepos(string login)
        {
            IsActive = true;

            PinnedItemsQueries queries = new();
            var PinnedItems = await queries.GetOverviewAll(login, true);

            foreach (var item in PinnedItems)
            {
                RepoButtonBlockViewModel viewModel = new();
                viewModel.Item = item;
                viewModel.DisplayDetails = false;
                viewModel.DisplayStarButton = false;

                PinnedRepos.Add(viewModel);
            }

            IsActive = false;
        }

        public async Task GetSpecialRepoId(string login)
        {
            try
            {
                var repo = await App.Client.Repository.Get(login, login);
                UserSpecialReadmeRepoId = repo.Id;
                HasUserSpecialReadmeRepoId = true;
            }
            catch
            {
                HasUserSpecialReadmeRepoId = false;
                return;
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
