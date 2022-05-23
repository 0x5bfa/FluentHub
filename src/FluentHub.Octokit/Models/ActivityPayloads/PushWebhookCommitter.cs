using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class PushWebhookCommitter
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
