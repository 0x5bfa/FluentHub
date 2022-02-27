using FluentHub.Models.Items;
using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Organizations
{
    public class RepoListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<RepoListItem> _items = new ObservableCollection<RepoListItem>();
        public ObservableCollection<RepoListItem> Items
        {
            get => _items;
            private set
            {
                _items = value;
                NotifyPropertyChanged(nameof(Items));
            }
        }

        public async void GetUserRepos(string username)
        {
            IsActive = true;

            ApiOptions options = new() { PageSize = 30, PageCount = 1, StartPage = 1 };

            var repos = await App.Client.Repository.GetAllForOrg(username, options);

            foreach (var item in repos)
            {
                RepoListItem listItem = new RepoListItem();
                listItem.RepoId = item.Id;
                Items.Add(listItem);
            }

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
