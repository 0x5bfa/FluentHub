using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graphqlmodel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Models
{
    public class IssueComment
    {
        public string AuthorLogin { get; set; }
        public string AuthorAvatarUrl { get; set; }
        public graphqlmodel.CommentAuthorAssociation AuthorAssociation { get; set; }
        public string BodyHtml { get; set; }
        public List<Reaction> Reactions { get; set; }
        public bool IsEdited { get; set; } // Different from original model "DateTimeOffset? LastEditedAt"
        public string MinimizedReason { get; set; }
        public bool IsMinimized { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool ViewerCanDelete { get; set; }
        public bool ViewerCanMinimize { get; set; }
        public bool ViewerCanReact { get; set; }
        public bool ViewerCanUpdate { get; set; }
        public bool ViewerDidAuthor { get; set; }
        public string Url { get; set; }
    }
}
