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

        public int GetPinnedRepos(List<long> repoIdList)
        {
            foreach (var repoId in repoIdList)
            {
                RepoListItem listItem = new RepoListItem();
                listItem.RepoId = repoId;
                Items.Add(listItem);
            }

            return Items.Count();
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
