using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLModel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Models.Events
{
    public class IssueComment
    {
        public Actor Author { get; set; }
        public string BodyHTML { get; set; }
        public string MinimizedReason { get; set; }
        public string Url { get; set; }

        public bool IsMinimized { get; set; }
        public bool ViewerCanDelete { get; set; }
        public bool ViewerCanMinimize { get; set; }
        public bool ViewerCanReact { get; set; }
        public bool ViewerCanUpdate { get; set; }
        public bool ViewerDidAuthor { get; set; }

        public List<Reaction> Reactions { get; set; } = new();
        public GraphQLModel.CommentAuthorAssociation AuthorAssociation { get; set; }

        public DateTimeOffset? LastEditedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
