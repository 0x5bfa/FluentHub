using System;
using System.Collections.Generic;

namespace FluentHub.Octokit.ModelGenerator.Models
{
    public class SchemaModel
    {
        public IList<TypeModel> Types { get; set; }
        public string QueryType { get; set; }
        public string MutationType { get; set; }
        public IList<DirectiveModel> Directives { get; set; }
    }
}
