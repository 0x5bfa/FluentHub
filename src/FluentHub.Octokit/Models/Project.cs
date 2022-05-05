using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLCore = global::Octokit.GraphQL.Core;
using GraphQLModel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Models
{
    public class Project
    {
        public string Body { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public int Number { get; set; }

        public bool Closed { get; set; }
        public bool ViewerCanUpdate { get; set; }

        public ProjectProgress Progress { get; set; }
        public Repository Repository { get; set; }
        public GraphQLModel.ProjectState State { get; set; }

        public DateTimeOffset? ClosedAt { get; set; }
        public string ClosedAtHumanized { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedAtHumanized { get; set; }
    }
}
