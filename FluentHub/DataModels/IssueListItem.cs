using FluentHub.Healper;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace FluentHub.DataModels
{
    public class IssueListItem
    {
        Issue issue;
        public bool IsOpened { get; private set; } = false;
        public string StatusGlyph { get; private set; } = "\uE9EA";
        public SolidColorBrush StatusGlyphForeground { get; set; } = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x57, 0xab, 0x5a));

        public string RepoFullName { get; private set; } = "[repoFullName]";
        public string TimeAgo { get; private set; } = "[timeAgo]";
        public string Title { get; private set; } = "[title]";

        public IssueListItem(Issue item)
        {
            issue = item;

            if (issue.State == ItemState.Open)
            {
                IsOpened = true;
                StatusGlyph = "\uE9E6";
                StatusGlyphForeground = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x82, 0x56, 0xd0));
            }

            var urlItem = issue.HtmlUrl.Split("/");

            RepoFullName = string.Format($"{urlItem[3]} / {urlItem[4]} • #{issue.Number}");
            TimeAgo = ConvertDataTimeToTimeAgo.GetDataTimeDiff(((DateTimeOffset)issue.UpdatedAt).ToLocalTime());
            Title = issue.Title;
        }
    }
}
