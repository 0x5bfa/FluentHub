using FluentHub.Helpers;
using FluentHub.Models.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class IssueCommentBlockViewModel : INotifyPropertyChanged
    {
        private long repositoryId;
        public long RepositoryId { get => repositoryId; set => SetProperty(ref repositoryId, value); }

        private int issueNumber;
        public int IssueNumber { get => issueNumber; set => SetProperty(ref issueNumber, value); }

        private long commentId;
        public long CommentId { get => commentId; set => SetProperty(ref commentId, value); }

        private IssueCommentItem issueComment;
        public IssueCommentItem IssueComment
        {
            get => issueComment;
            set
            {
                SetProperty(ref issueComment, value);
            }
        }

        private string bodyHtml;
        public string BodyHtml { get => bodyHtml; set => SetProperty(ref bodyHtml, value); }

        public async Task SetWebViewContentsAsync(WebView webView)
        {
            Octokit.Queries.MarkdownQueries markdown = new();

            var repo = await App.Client.Repository.Get(RepositoryId);

            var html = await markdown.GetHtml(IssueComment.Body, repo.HtmlUrl, ThemeHelper.ActualTheme.ToString().ToLower());

            webView.NavigateToString(html);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
