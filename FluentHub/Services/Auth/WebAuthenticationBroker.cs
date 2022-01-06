using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Services.Auth
{
    public class WebAuthenticationBroker
    {
        public WebAuthenticationBroker()
        {
            // Get OAuth app key/secret
            AppCredentials appCredentials = new AppCredentials();
            _ = appCredentials.GetAppCredentials();
        }
    }
}
