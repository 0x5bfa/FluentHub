using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLModel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Models
{
    public class PullRequest
    {
        public string Name { get; set; }
        public string OwnerLogin { get; set; }
        public string OwnerAvatarUrl { get; set; }
        public string Title { get; set; }
        public string BaseRefName { get; set; }
        public string HeadRefName { get; set; }
        public string HeadRefOwnerLogin { get; set; }

        public int Number { get; set; }
        public int CommentCount { get; set; }

        public bool Closed { get; set; }
        public bool Merged { get; set; }
        public bool IsDraft { get; set; }

        public List<Label> Labels { get; set; } = new();

        public GraphQLModel.PullRequestReviewState ReviewState { get; set; }
        public StatusCheckRollup StatusState { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedAtHumanized { get; set; }
    }
}
