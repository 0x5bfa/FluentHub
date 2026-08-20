using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace FluentHub.App.Services
{
	public static class OctokitSecretsService
	{
		public static async Task<Octokit.Authorization.OctokitSecrets?> LoadOctokitSecretsAsync()
		{
			StorageFile file;

			try
			{
				file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///AppCredentials.config"));
			}
			catch (System.IO.FileNotFoundException)
			{
				return null;
			}

			var xmlDoc = await XmlDocument.LoadFromFileAsync(file);

			var nodeId = xmlDoc.DocumentElement.SelectSingleNode("./client/type[@key='id']/@value");
			var nodeSecret = xmlDoc.DocumentElement.SelectSingleNode("./client/type[@key='secret']/@value");

			if (string.IsNullOrEmpty(nodeId?.NodeValue as string))
			{
				return null;
			}

			return new Octokit.Authorization.OctokitSecrets()
			{
				ClientId = (string)nodeId.NodeValue,
				ClientSecret = nodeSecret?.NodeValue as string ?? string.Empty,
			};
		}
	}
}
