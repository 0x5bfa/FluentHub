using FluentHub.Helpers;
using FluentHub.Octokit.Models.Events;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.UserControls.Labels;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class IssueCommentBlockViewModel : ObservableObject
    {
        public IssueCommentBlockViewModel()
        {
            IsEditedLabel = new()
            {
                Color = "#36000000",
                Name = "Edited",
            };

            AuthorAssociationLabel = new()
            {
                Color = "#36000000",
            };
        }

        #region Fields and Properties
        private IssueComment issueComment;
        public IssueComment IssueComment { get => issueComment; set => SetProperty(ref issueComment, value); }

        private string createdAtHumanized;
        public string CreatedAtHumanized { get => createdAtHumanized; set => SetProperty(ref createdAtHumanized, value); }

        private LabelControlViewModel _isEditedLabel;
        public LabelControlViewModel IsEditedLabel { get => _isEditedLabel; set => SetProperty(ref _isEditedLabel, value); }

        private LabelControlViewModel _authorAssociationLabel;
        public LabelControlViewModel AuthorAssociationLabel { get => _authorAssociationLabel; set => SetProperty(ref _authorAssociationLabel, value); }
        #endregion

        public async Task SetWebViewContentsAsync(WebView webView)
        {
            CreatedAtHumanized = IssueComment?.CreatedAt.Humanize();

            string authorAssociation = IssueComment?.AuthorAssociation.Humanize();
            if (authorAssociation != "None") AuthorAssociationLabel.Name = authorAssociation;

            MarkdownQueries markdown = new();
            var html = await markdown.GetHtmlAsync(IssueComment?.BodyHTML, IssueComment?.Url, ThemeHelper.ActualTheme.ToString().ToLower(), true);
            webView.NavigateToString(html);
        }
    }
}
