using FluentHub.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserPage
{
    public class RepositoriesPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<RepoItem> _items = new ObservableCollection<RepoItem>();
        public ObservableCollection<RepoItem> Items
        {
            get => _items;
            private set
            {
                _items = value;
                NotifyPropertyChanged(nameof(Items));
            }
        }

        public RepositoriesPageViewModel()
        {
        }

        public async void GetUserRepos()
        {
            var repos = await App.Client.Repository.GetAllForCurrent();

            foreach (var item in repos)
            {
                if (item.Owner.Type == Octokit.AccountType.User)
                {
                    RepoItem listItem = new RepoItem(item);
                    Items.Add(listItem);
                }
            }

            Items = new ObservableCollection<RepoItem>(Items.OrderByDescending(x => x.UpdatedAt));

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
