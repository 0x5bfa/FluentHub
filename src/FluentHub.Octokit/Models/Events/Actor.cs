using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.Events
{
    public class Actor
    {
        public string Login { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsUser { get; set; }
    }
}
