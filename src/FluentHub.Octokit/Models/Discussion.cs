using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLCore = global::Octokit.GraphQL.Core;
using GraphQLModel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Models
{
    public class Discussion
    {
        public bool IncludesCreatedEdit { get; set; }
        public bool Locked { get; set; }
        public bool ViewerCanDelete { get; set; }
        public bool ViewerCanReact { get; set; }
        public bool ViewerCanSubscribe { get; set; }
        public bool ViewerCanUpdate { get; set; }
        public bool ViewerCanUpvote { get; set; }
        public bool ViewerDidAuthor { get; set; }
        public bool ViewerHasUpvoted { get; set; }

        public int Number { get; set; }
        public int TotalCommentCount { get; set; }
        public int UpvoteCount { get; set; }

        public string BodyHTML { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public GraphQLModel.LockReason? ActiveLockReason { get; set; }
        public GraphQLModel.CommentAuthorAssociation AuthorAssociation { get; set; }
        public DiscussionCategory Category { get; set; }
        public Repository Repository { get; set; }
        public GraphQLModel.SubscriptionState? ViewerSubscription { get; set; }

        public DateTimeOffset? AnswerChosenAt { get; set; }
        public string AnswerChosenAtHumanized { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
        public DateTimeOffset? LastEditedAt { get; set; }
        public string LastEditedAtHumanized { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public string PublishedAtHumanized { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedAtHumanized { get; set; }
    }
}
