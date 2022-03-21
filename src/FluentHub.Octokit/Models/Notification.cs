using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class Notification
    {
        public long Id { get; protected set; }

        public global::Octokit.Repository Repository { get; protected set; }

        public global::Octokit.NotificationInfo Subject { get; protected set; }

        public string Reason { get; protected set; }

        public bool Unread { get; protected set; }

        public DateTimeOffset UpdatedAt { get; protected set; }

        public DateTimeOffset LastReadAt { get; protected set; }

        public string Url { get; protected set; }
    }
}
