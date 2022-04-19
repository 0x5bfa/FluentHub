using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLModel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Models.Events
{
    public class UserBlockedEvent
    {
        public Actor Actor { get; set; }

        public GraphQLModel.UserBlockDuration BlockDuration { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
    }
}
