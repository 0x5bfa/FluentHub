using FluentHub.Models.Items;
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
    public class OverviewViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<RepoListItem> _items = new ObservableCollection<RepoListItem>();
        public ObservableCollection<RepoListItem> Items
        {
            get => _items;
            private set
            {
                SetProperty(ref _items, value);
            }
        }

        public int GetPinnedRepos(List<long> repoIdList)
        {
            IsEnabled = false;

            foreach (var repoId in repoIdList)
            {
                IsEnabled = false;
                RepoListItem listItem = new RepoListItem();
                listItem.RepoId = repoId;
                Items.Add(listItem);
                IsEnabled = true;
            }

           IsEnabled = true;
            return Items.Count();

            
        }

        #region PropetyChanged things

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

        private bool isEnabled;
        public bool IsEnabled { get => isEnabled; set => SetProperty(ref isEnabled, value); }

        #endregion
    }
}
