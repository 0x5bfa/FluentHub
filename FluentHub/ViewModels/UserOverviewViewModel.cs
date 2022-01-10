using FluentHub.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels
{
    public class UserOverviewViewModel
    {
        public static ObservableCollection<RepoItem> Items { get; private set; }

        public UserOverviewViewModel()
        {
            Items = new ObservableCollection<RepoItem>();
        }

        public void GetUserPinnedRepos()
        {

        }
    }
}
