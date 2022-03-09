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
        public ObservableCollection<Repository> Items { get; private set; }

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public async Task GetUserRepos(string username)
        {
            IsActive = true;

            ApiOptions options = new() { PageSize = 30, PageCount = 1, StartPage = 1 };

            var repos = await App.Client.Repository.GetAllForUser(username, options);

            foreach (var item in repos)
            {
                if (Items.Count == 30) break;

                if (item.Owner.Type == AccountType.User)
                {
                    Items.Add(item);
                }
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
    }
}
