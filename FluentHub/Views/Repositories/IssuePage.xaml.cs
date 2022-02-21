using FluentHub.Models.Items;
using FluentHub.UserControls.Issue;
using FluentHub.ViewModels.UserControls.Issue;
using Humanizer;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Repositories
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

            SetIssueBody();

            await SetLabelContent();

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

        public struct IssueItemStruct
        {
            public string Type;
            public DateTimeOffset CreatedAt;
            public IssueComment Comment;
            public IssueEvent Event;
            public PullRequestCommit Commit;
        }

        private async Task SetIssueContent()
        {
            var comments = await App.Client.Issue.Comment.GetAllForIssue(RepoId, IssueNumber);
            var events = await App.Client.Issue.Events.GetAllForIssue(RepoId, IssueNumber);

            IReadOnlyList<PullRequestCommit> commits;

            if (Issue.PullRequest != null)
            {
                commits = await App.Client.PullRequest.Commits(RepoId, IssueNumber);
            }

            List<IssueItemStruct> issueItems = new();

            foreach (var commentItem in comments)
            {
                IssueItemStruct issueItemStruct = new() { Type = "Comment", CreatedAt = commentItem.CreatedAt, Comment = commentItem };
                issueItems.Add(issueItemStruct);
            }

            foreach (var eventItem in events)
            {
                IssueItemStruct issueItemStruct = new() { Type = "Event", CreatedAt = eventItem.CreatedAt, Event = eventItem };
                issueItems.Add(issueItemStruct);
            }

            //foreach (var commitItem in commits)
            //{
            //    IssueItemStruct issueItemStruct = new() { Type = "Commit", CreatedAt = commitItem.Commit.Author.Date, Commit = commitItem };
            //}

            var sortedIssueItems = issueItems.OrderBy(x => x.CreatedAt);

            foreach (var item in sortedIssueItems)
            {
                if (item.Type == "Comment")
                {
                    var viewmodel = new IssueCommentBlockViewModel();
                    var commentItem = new IssueCommentItem();

                    viewmodel.CommentId = item.Comment.Id;
                    viewmodel.RepositoryId = RepoId;
                    viewmodel.IssueNumber = IssueNumber;

                    commentItem.AuthorAssociation = item.Comment.AuthorAssociation;
                    commentItem.Body = item.Comment.Body;
                    commentItem.CreatedAt = item.CreatedAt.Humanize();
                    commentItem.HtmlUrl = item.Comment.HtmlUrl;
                    commentItem.Id = item.Comment.Id;
                    commentItem.NodeId = item.Comment.NodeId;
                    commentItem.Reactions = item.Comment.Reactions;
                    commentItem.UpdatedAt = item.Comment.UpdatedAt.Humanize();
                    commentItem.Url = item.Comment.Url;
                    commentItem.User = item.Comment.User;

                    viewmodel.IssueComment = commentItem;

                    var commentBlock = new IssueCommentBlock() { PropertyViewModel = viewmodel };
                    IssueContentPanel.Children.Add(commentBlock);
                }
                else
                {
                    var viewmodel = new IssueEventBlockViewModel();
                    var eventItem = new IssueEventItem();

                    viewmodel.CommentId = item.Event.Id;
                    viewmodel.RepositoryId = RepoId;
                    viewmodel.IssueNumber = IssueNumber;

                    if (item.Type == "Commit")
                    {
                        eventItem.Actor = item.Commit.Author;
                        eventItem.CommitId = item.Commit.Commit.Sha;
                        eventItem.CommitUrl = item.Commit.Commit.Url;
                        eventItem.NodeId = item.Commit.NodeId;
                        eventItem.Url = item.Commit.Url;
                    }
                    else
                    {
                        eventItem.Actor = item.Event.Actor;
                        eventItem.Assignee = item.Event.Assignee;
                        eventItem.CommitId = item.Event.CommitId;
                        eventItem.CommitUrl = item.Event.CommitUrl;
                        eventItem.CreatedAt = item.Event.CreatedAt;
                        eventItem.Event = item.Event.Event;
                        eventItem.Id = item.Event.Id;
                        eventItem.Issue = item.Event.Issue;
                        eventItem.Label = item.Event.Label;
                        eventItem.NodeId = item.Event.NodeId;
                        eventItem.ProjectCard = item.Event.ProjectCard;
                        eventItem.Rename = item.Event.Rename;
                        eventItem.Url = item.Event.Url;
                    }

                    viewmodel.IssueEvent = eventItem;

                    var eventBlock = new IssueEventBlock() { PropertyViewModel = viewmodel };
                    IssueContentPanel.Children.Add(eventBlock);
                }
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
