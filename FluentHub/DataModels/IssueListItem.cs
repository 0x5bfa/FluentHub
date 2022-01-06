using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.DataModels
{
    internal class IssueListItem
    {
        // Format: 5d (means 5 days ago)
        //         3mo (means 3 month ago)
        private string timeAgo;
        public string TimeAgo
        {
            get
            {
                return timeAgo;
            }
            set
            {
                timeAgo = value;
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
    }
}
