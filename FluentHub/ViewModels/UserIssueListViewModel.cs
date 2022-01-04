using FluentHub.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels
{
    internal class UserIssueListViewModel
    {
        public static ObservableCollection<IssueListItem> ItemsAdded = new ObservableCollection<IssueListItem>();


    }
}
