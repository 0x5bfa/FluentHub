using FluentHub.DataModels;
using FluentHub.UserControls;
using FluentHub.ViewModels;
using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace FluentHub.ViewModels.UserPages
{
    public class IssuesViewModel
    {
        public static ObservableCollection<IssueListItem> Items { get; private set; }

        public IssuesViewModel()
        {
            Items = new ObservableCollection<IssueListItem>();
        }

        public async void GetUserIssues()
        {
            SearchIssuesRequest request = new SearchIssuesRequest();
            request.Author = "onein528";
            var Issues = await App.Client.Search.SearchIssues(request);

            foreach(var item in Issues.Items)
            {
                IssueListItem listItem = new IssueListItem(item);
                Items.Add(listItem);
            }
        }
    }
}
