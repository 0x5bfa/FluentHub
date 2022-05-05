using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class Notification
    {
        public global::Octokit.Repository Repository { get; set; }
        public global::Octokit.NotificationInfo Subject { get; set; }

        public string Reason { get; set; }
        public string Url { get; set; }
        public string ItemState { get; set; }

        public bool Unread { get; set; }
        public long Id { get; set; }
        public int SubjectNumber { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedAtHumanized { get; set; }

        public DateTimeOffset LastReadAt { get; set; }
    }
}
