using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace FluentHub.ViewModels.RepoPages
{
    public class PullRequestListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PRListItem> _items = new ObservableCollection<PRListItem>();

        public ObservableCollection<PRListItem> Items
        {
            get => _items;
            private set
            {
                _items = value;
                NotifyPropertyChanged(nameof(Items));

            }
        }

        public async void GetRepoPullRequests(long repoId)
        {
            IsActive = true;

            var pulls = await App.Client.PullRequest.GetAllForRepository(repoId);

            foreach (var item in pulls)
            {
                PRListItem listItem = new PRListItem();
                listItem.RepoId = repoId;
                listItem.PRIndex = item.Number;
                Items.Add(listItem);
            }
        }

        #region Properties and IsActive state
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
        #endregion
    }

    public class PRListItem
    {
        public long RepoId { get; set; }

        public int PRIndex{ get; set; }
    }
}
