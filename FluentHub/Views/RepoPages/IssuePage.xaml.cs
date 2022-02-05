using Humanizer;
using Octokit;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.RepoPages
{
    public sealed partial class IssuePage : Windows.UI.Xaml.Controls.Page
    {
        private long RepoId { get; set; }
        private int IssueNumber { get; set; }
        private Issue Issue { get; set; }

        public IssuePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = (e.Parameter as string).Split("/");

            RepoId = Convert.ToInt64(param[0] as string);
            IssueNumber = Convert.ToInt32(param[1] as string);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Issue = await App.Client.Issue.Get(RepoId, IssueNumber);

            IssueTitleTextBlock.Text = Issue.Title;
            IssueNumberTextBlock.Text = "#" + IssueNumber.ToString();

            IssueOpenerNameTextBlock.Text = Issue.User.Login;
            IssueOpenedComment.Text = "opened this issue " + Issue.CreatedAt.Humanize() + " · " + Issue.Comments.ToString() + " comments";

            var events = await App.Client.Issue.Events.GetAllForIssue(RepoId, IssueNumber);

            foreach (var item in events)
            {

            }
        }
    }
}
