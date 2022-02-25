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

namespace FluentHub.ViewModels.Users
{
    public class RepoListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Repository> _items = new ObservableCollection<Repository>();
        public ObservableCollection<Repository> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public async Task<int> GetUserRepos(string username)
        {
            IsActive = true;

            ApiOptions options = new() { PageSize = 30, PageCount = 1, StartPage = 1 };

            var repos = await App.Client.Repository.GetAllForUser(username, options);

            foreach (var item in repos)
            {
                // In the future, pagination will be avaiable https://primer.style/react/Pagination
                if (Items.Count == 30) break;

                if (item.Owner.Type == AccountType.User)
                {
                    Items.Add(item);
                }
            }

            // order by updated at
            //Items = new(Items.OrderByDescending(x => x.UpdatedAt));

            IsActive = false;

            return repos.Count();
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
