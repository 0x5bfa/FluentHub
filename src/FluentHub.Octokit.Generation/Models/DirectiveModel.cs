using System;
using System.Collections.Generic;
using Octokit.GraphQL.Core.Introspection;

namespace FluentHub.Octokit.Generation.Models
{
    public class DirectiveModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<DirectiveLocation> Locations { get; set; }
        public IList<InputValueModel> Args { get; set; }
    }
}
