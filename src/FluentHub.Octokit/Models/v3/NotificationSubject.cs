using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHub.Octokit.Models.v3
{
    public class NotificationSubject
    {
        public NotificationSubjectType Type { get; set; }

        public int Number { get; set; }

        public string Title { get; set; }
    }
}
