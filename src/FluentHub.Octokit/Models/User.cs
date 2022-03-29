using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class User
    {
        public string AvatarUrl { get; set; }
        public string Bio { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public bool IsCampusExpert { get; set; }
        public bool IsBountyHunter { get; set; }
        public bool IsDeveloperProgramMember { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsGitHubStar { get; set; }
        public bool IsViewer { get; set; }
        public string Location { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string TwitterUsername { get; set; }
        public bool ViewerIsFollowing { get; set; }
        public string WebsiteUrl { get; set; }
        public bool IsOrganization { get; set; }

        public int FollowersTotalCount { get; set; }
        public int FollowingTotalCount { get; set; }
    }
}
