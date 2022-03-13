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

namespace FluentHub.ViewModels.Organizations
{
    public class OverviewViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<RepoListItem> OrgPinnedItems { get; private set; } = new();

        public async Task GetPinnedRepos(string org)
        {
            UserPinnedItems pinnedItems = new UserPinnedItems();
            var repoIdList = await pinnedItems.Get(org, false);

            foreach (var repoId in repoIdList)
            {
                RepoListItem listItem = new RepoListItem();
                listItem.RepoId = repoId;
                OrgPinnedItems.Add(listItem);
            }
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
