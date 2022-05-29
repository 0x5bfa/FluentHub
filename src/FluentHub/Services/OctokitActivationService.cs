using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.System;

namespace FluentHub.Services
{
    public class OctokitActivationService
    {
        public static async Task<Octokit.Authorization.OctokitSecret> GetOctokitSecrets()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///AppCredentials.config"));

            var xmlDoc = await XmlDocument.LoadFromFileAsync(file);

            var nodeId = xmlDoc.DocumentElement.SelectSingleNode("./client/type[@key='id']/@value");
            var nodeSecret = xmlDoc.DocumentElement.SelectSingleNode("./client/type[@key='secret']/@value");

            return new Octokit.Authorization.OctokitSecret()
            {
                ClientId = (string)nodeId.NodeValue,
                ClientSecret = (string)nodeSecret.NodeValue,
            };
        }
    }
}
