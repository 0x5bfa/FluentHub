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

namespace FluentHub.ViewModels
{
    public class UserIssueListViewModel
    {
        public static ObservableCollection<IssueListItem> Items { get; private set; }

        public UserIssueListViewModel()
        {
            Items = new ObservableCollection<IssueListItem>();
        }

        public async void GetUserIssues()
        {
            Services.Octiokit.UserClient.GithubClient = new GitHubClient(new ProductHeaderValue("FluentHub")) { Credentials = new Credentials("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") };
            SearchIssuesRequest request = new SearchIssuesRequest();
            request.Author = "onein528";
            var Issues = await Services.Octiokit.UserClient.GithubClient.Search.SearchIssues(request);

            foreach(var item in Issues.Items)
            {
                IssueListItem listItem = new IssueListItem(item);
                Items.Add(listItem);
            }
        }
    }
}
