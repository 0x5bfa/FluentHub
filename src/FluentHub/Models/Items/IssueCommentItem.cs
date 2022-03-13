using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Models.Items
{
    public class IssueCommentItem
    {
        public int Id { get; set; }

        public string NodeId { get; set; }

        public string Url { get; set; }

        public string HtmlUrl { get; set; }

        public string Body { get; set; }

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public User User { get; set; }

        public StringEnum<AuthorAssociation> AuthorAssociation { get; set; }

        public ReactionSummary Reactions { get; set; }
    }
}
