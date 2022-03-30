using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.Repositories;
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

        private RepoContextViewModel contextViewModel;
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public async Task GetPinnedRepos(string login)
        {
            IsActive = true;

            PinnedItemQueries queries = new();
            var items = await queries.GetAllAsync(login, true);

            if (items == null)
            {
                IsActive = false;
                return;
            }

            foreach (var item in items)
            {
                RepoButtonBlockViewModel viewModel = new();
                viewModel.Item = item;
                viewModel.DisplayDetails = false;
                viewModel.DisplayStarButton = false;

                PinnedRepos.Add(viewModel);
            }

            RepoContextViewModel readmeRepoViewModel = new();
            readmeRepoViewModel.Owner = login;
            readmeRepoViewModel.Name = login;
            ContextViewModel = readmeRepoViewModel;

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
