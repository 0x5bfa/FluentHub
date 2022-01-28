using FluentHub.Models.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserPages
{
    public class OverviewViewModel : INotifyPropertyChanged
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

        public async void GetUserRepos(List<long> repoIdList)
        {
            foreach (var repoId in repoIdList)
            {
                var item = await App.Client.Repository.Get(repoId);

                RepoListItem listItem = new RepoListItem(item);
                Items.Add(listItem);
            }

            Items = new ObservableCollection<RepoListItem>(Items.OrderByDescending(x => x.UpdatedAt));
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
