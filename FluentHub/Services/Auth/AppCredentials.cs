using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace FluentHub.Services.Auth
{
    public class AppCredentials
    {
        private string credentialsLocation = "ms-appx:///AppCredentials.config";
        public string clientId = "";
        public string clientSecret = "";

        public AppCredentials()
        {
            clientId = "";
            clientSecret = "";
        }

        public async Task GetAppCredentials()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(credentialsLocation));

            var xmlConfiguration = await XmlDocument.LoadFromFileAsync(file);

            var nodeId = xmlConfiguration.DocumentElement.SelectSingleNode("./client/type[@key='id']/@value");
            var nodeSecret = xmlConfiguration.DocumentElement.SelectSingleNode("./client/type[@key='secret']/@value");

            clientId = (string)nodeId.NodeValue;
            clientSecret = (string)nodeSecret.NodeValue;
        }
    }
}
