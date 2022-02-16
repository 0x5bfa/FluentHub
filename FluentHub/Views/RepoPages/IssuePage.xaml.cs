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

            if (Issue.PullRequest != null)
            {
                var pr = await App.Client.PullRequest.Get(RepoId, IssueNumber);

                if (pr.Merged)
                {
                    StatusLabel.Status = UserControls.Labels.Status.PullMerged;
                }
                else if (pr.Draft)
                {
                    StatusLabel.Status = UserControls.Labels.Status.PullDraft;
                }
                else
                {
                    switch (pr.State.Value.ToString())
                    {
                        case "Open":
                            StatusLabel.Status = UserControls.Labels.Status.PullOpened;
                            break;
                        case "Closed":
                            StatusLabel.Status = UserControls.Labels.Status.PullClosed;
                            break;
                    }
                }
            }
            else
            {
                switch (Issue.State.Value.ToString())
                {
                    case "Open":
                        StatusLabel.Status = UserControls.Labels.Status.IssueOpened;
                        break;
                    case "Closed":
                        StatusLabel.Status = UserControls.Labels.Status.IssueClosed;
                        break;
                }
            }

            var events = await App.Client.Issue.Events.GetAllForIssue(RepoId, IssueNumber);

            foreach (var item in events)
            {

            }
        }
    }
}
