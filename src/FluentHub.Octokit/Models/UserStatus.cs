using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class UserStatus
    {
        public string Emoji { get; set; }

        public DateTimeOffset? ExpiresAt { get; set; }

        public string Message { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
