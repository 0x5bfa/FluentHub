using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FluentHub.Models.Items
{
    public class NotificationListItem
    {
        public string Tag { get; set; } // Format: "RepoId/SubjectId" (SubjectId: issue, pr, or discussion id)
        public string SubjectStateIconGlyph { get; set; } = "\uE9EA";
        public Brush SubjectStateIconForeground { get; set; }
        public string SubjectWithOwnerName { get; set; }
        public int SubjectId { get; set; }
        public string FormattedNotifiedDate { get; set; } = "2h";
        public string SubjectTitle { get; set; }
        public string FormattedNotifiedReason { get; set; }
        public Visibility Unread { get; set; }
    }
}
