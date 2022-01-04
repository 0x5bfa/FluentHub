using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Services.Octiokit
{
    public class UserClient
    {
        public static GitHubClient GithubClient { get; set; }
    }
}
