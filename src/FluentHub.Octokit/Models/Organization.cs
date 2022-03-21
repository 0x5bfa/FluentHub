using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class Organization
    {
        public string AvatarUrl { get; set; } // 100x100

        public string Description { get; set; }

        public string Email { get; set; }

        public bool IsVerified { get; set; }

        public string Location { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string WebsiteUrl { get; set; }
    }
}
