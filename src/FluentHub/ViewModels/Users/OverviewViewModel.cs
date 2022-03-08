using FluentHub.Models.Items;
using FluentHub.Services.OctokitEx;
using Serilog;
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
    public class OverviewViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<RepoListItem> Items { get; private set; } = new();

        public async Task<int> GetPinnedRepos(string login)
        {
            try
            {
                UserPinnedItems pinnedItems = new UserPinnedItems();

                var repoIdList = await pinnedItems.Get(login, true);

                foreach (var repoId in repoIdList)
                {
                    RepoListItem listItem = new RepoListItem();
                    listItem.RepoId = repoId;
                    Items.Add(listItem);
                }

                return Items.Count();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return -1;
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
