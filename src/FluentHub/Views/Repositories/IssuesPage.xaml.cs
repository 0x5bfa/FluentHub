using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Repositories
{
    public sealed partial class IssuesPage : Page
    {
        public IssuesPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            long repositoryId = Convert.ToInt64(e.Parameter as string);

            var repo = await App.Client.Repository.Get(repositoryId);
            await ViewModel.GetRepoIssues(repo.Name, repo.Owner.Login);
        }
    }
}
