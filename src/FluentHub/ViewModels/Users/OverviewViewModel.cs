using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.Blocks;
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

        private ReadmeContentBlockViewModel readmeBlockViewModel;
        public ReadmeContentBlockViewModel ReadmeBlockViewModel { get => readmeBlockViewModel; set => SetProperty(ref readmeBlockViewModel, value); }

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public async Task GetPinnedRepos(string login)
        {
            IsActive = true;

            PinnedItemsQueries queries = new();
            var items = await queries.GetAllAsync(login, true);

            if (items == null) return;

            foreach (var item in items)
            {
                RepoButtonBlockViewModel viewModel = new();
                viewModel.Item = item;
                viewModel.DisplayDetails = false;
                viewModel.DisplayStarButton = false;

                PinnedRepos.Add(viewModel);
            }

            ReadmeContentBlockViewModel readmeViewModel = new();
            readmeViewModel.Owner = login;
            readmeViewModel.RepoName = login;
            ReadmeBlockViewModel = readmeViewModel;

            IsActive = false;
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
