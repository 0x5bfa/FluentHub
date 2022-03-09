using FluentHub.Models.Items;
using FluentHub.Services.OctokitEx;
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
    public class StarListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<RepoListItem> Items { get; private set; }

        public async void GetUserStarredRepos(string username)
        {
            IsActive = true;

            UserStarredItems starredItems = new UserStarredItems();
            var repoIdList = await starredItems.Get(username);

            foreach (var repoId in repoIdList)
            {
                RepoListItem listItem = new RepoListItem();
                listItem.RepoId = repoId;

                Items.Add(listItem);
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
