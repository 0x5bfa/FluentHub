using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class User
    {
        public string AvatarUrl { get; set; } // 100x100

        public string Login { get; set; }

        public string Name { get; set; }

        public string Bio { get; set; }
    }
}
