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

namespace FluentHub.ViewModels.UserPages
{
    public class StarListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<RepoListItem> _items;
        public ObservableCollection<RepoListItem> Items
        {
            get => _items;
            private set
            {
                SetProperty(ref _items, value);
            }
        }

        public async void GetUserStarredRepos(string username)
        {
            IsActive = true;
            Items = new ObservableCollection<RepoListItem>();
            UserStarredItems starredItems = new UserStarredItems();

            var repoIdList = await starredItems.Get(username);

            foreach (var repoId in repoIdList)
            {
                RepoListItem listItem = new RepoListItem();
                listItem.RepoId = repoId;
                Items.Add(listItem);
            }

            SetProperty(ref _items, Items);

            IsActive = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

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
