using FluentHub.Models.Items;
using FluentHub.UserControls.Issue;
using FluentHub.ViewModels.UserControls.Issue;
using Humanizer;
using Octokit;
using System;
using System.Threading.Tasks;
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

            await SetLabelContent();

            SetIssueBody();

            await SetIssueContent();
        }

        private async Task SetLabelContent()
        {
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
        }

        private async Task SetIssueContent()
        {
            var comments = await App.Client.Issue.Comment.GetAllForIssue(RepoId, IssueNumber);

            foreach (var item in comments)
            {
                var viewmodel = new IssueCommentBlockViewModel();
                var commentItem = new IssueCommentItem();

                viewmodel.CommentId = item.Id;
                viewmodel.RepositoryId = RepoId;
                viewmodel.IssueNumber = IssueNumber;

                commentItem.AuthorAssociation = item.AuthorAssociation;
                commentItem.Body = item.Body;
                commentItem.CreatedAt = item.CreatedAt.Humanize();
                commentItem.HtmlUrl = item.HtmlUrl;
                commentItem.Id = item.Id;
                commentItem.NodeId = item.NodeId;
                commentItem.Reactions = item.Reactions;
                commentItem.UpdatedAt = item.UpdatedAt.Humanize();
                commentItem.Url = item.Url;
                commentItem.User = item.User;

                viewmodel.IssueComment = commentItem;

                var commentBlock = new IssueCommentBlock() { PropertyViewModel = viewmodel };
                IssueContentPanel.Children.Add(commentBlock);
            }
        }

        private void SetIssueBody()
        {
            var viewmodel = new IssueCommentBlockViewModel();
            var commentItem = new IssueCommentItem();

            viewmodel.CommentId = Issue.Id;
            viewmodel.RepositoryId = RepoId;
            viewmodel.IssueNumber = IssueNumber;

            commentItem.Body = Issue.Body;
            commentItem.CreatedAt = Issue.CreatedAt.Humanize();
            commentItem.HtmlUrl = Issue.HtmlUrl;
            commentItem.Id = Issue.Id;
            commentItem.NodeId = Issue.NodeId;
            commentItem.Reactions = Issue.Reactions;
            commentItem.UpdatedAt = Issue.UpdatedAt.Humanize();
            commentItem.Url = Issue.Url;
            commentItem.User = Issue.User;

            viewmodel.IssueComment = commentItem;

            var commentBlock = new IssueCommentBlock() { PropertyViewModel = viewmodel };
            IssueContentPanel.Children.Add(commentBlock);
        }
    }
}
