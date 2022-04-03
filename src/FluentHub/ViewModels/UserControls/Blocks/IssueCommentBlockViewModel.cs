using FluentHub.Helpers;
using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Repositories;
using Humanizer;
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
        private IssueComment issueComment;
        public IssueComment IssueComment
        {
            get => issueComment;
            set => SetProperty(ref issueComment, value);
        }

        private string createdAtHumanized;
        public string CreatedAtHumanized { get => createdAtHumanized; set => SetProperty(ref createdAtHumanized, value); };

        public async Task SetWebViewContentsAsync(WebView webView)
        {
            CreatedAtHumanized = IssueComment.CreatedAt.Humanize();

            MarkdownQueries markdown = new();
            var html = await markdown.GetHtmlAsync(IssueComment.BodyHtml, IssueComment.Url, ThemeHelper.ActualTheme.ToString().ToLower());
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
