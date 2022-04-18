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
        public string ReviewState { get; set; }
        public string StatusState { get; set; }

        public int Number { get; set; }
        public int CommentCount { get; set; }

        public bool IsClosed { get; set; }
        public bool IsMerged { get; set; }
        public bool IsDraft { get; set; }

        public List<Label> Labels { get; set; } = new();

        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedAtHumanized { get; set; }
    }
}
