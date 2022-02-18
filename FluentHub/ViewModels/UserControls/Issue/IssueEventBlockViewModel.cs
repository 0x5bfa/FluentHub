using FluentHub.Models.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserControls.Issue
{
    public class IssueEventBlockViewModel : INotifyPropertyChanged
    {
        private long repositoryId;
        public long RepositoryId { get => repositoryId; set => SetProperty(ref repositoryId, value); }

        private int issueNumber;
        public int IssueNumber { get => issueNumber; set => SetProperty(ref issueNumber, value); }

        private long commentId;
        public long CommentId { get => commentId; set => SetProperty(ref commentId, value); }

        private IssueEventItem issueEvent;
        public IssueEventItem IssueEvent
        {
            get => issueEvent;
            set
            {
                SetProperty(ref issueEvent, value);
            }
        }

        private string bodyHtml;
        public string BodyHtml { get => bodyHtml; set => SetProperty(ref bodyHtml, value); }

        //public async Task SetWebViewContentsAsync(WebView webView)
        //{
        //    Markdown markdown = new();

        //    var repo = await App.Client.Repository.Get(RepositoryId);

        //    var BodyHtml = await markdown.GetHtml(IssueComment.Body, repo.HtmlUrl);

        //    webView.NavigateToString(BodyHtml);
        //}

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
