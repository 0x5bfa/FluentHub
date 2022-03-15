using FluentHub.OctokitEx.Queries.User;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Octokit;
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
    public class StarredReposViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<RepoButtonBlockViewModel> Items { get; private set; } = new();

        public async void GetUserStarredRepos(string login)
        {
            IsActive = true;

            EnumStarredRepoQueries queries = new();
            var items = await queries.Get(login);

            foreach (var item in items)
            {
                RepoButtonBlockViewModel viewModel = new();
                viewModel.Item = item;
                viewModel.DisplayDetails = true;
                viewModel.DisplayStarButton = true;

                Items.Add(viewModel);
            }

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

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }
    }
}
