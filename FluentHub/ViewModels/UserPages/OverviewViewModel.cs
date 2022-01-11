using FluentHub.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserPages
{
    public class OverviewViewModel
    {
        public static ObservableCollection<RepoItem> Items { get; private set; }

        public OverviewViewModel()
        {
            Items = new ObservableCollection<RepoItem>();
        }

        public void GetUserPinnedRepos()
        {

        }
    }
}
