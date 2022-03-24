using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class ApiOptions
    {
        public bool UseOctokit { get; set; }
        public bool UseOctokitGraphQL { get; set; }

        // For Octokit.NET
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int StartPage { get; set; }

        // For Octokit.GraphQL.NET
        public string After { get; set; }
        public string Before { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }
}
