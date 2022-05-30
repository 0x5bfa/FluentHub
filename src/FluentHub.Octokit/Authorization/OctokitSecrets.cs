using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHub.Octokit.Authorization
{
    public class OctokitSecrets
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public bool IsVaild()
        {
            if (string.IsNullOrEmpty(ClientId) || string.IsNullOrEmpty(ClientSecret))
            {
                return false;
            }

            return true;
        }
    }
}
