using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FluentHub.Services.Auth
{
    public class AppCredentials
    {
        [JsonPropertyName("clientId")]
        public string ClientId { get; private set; } = null;
        [JsonPropertyName("clientSecret")]
        public string ClientSecret { get; private set; } = null;

        public async Task GetAppCredentials()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile file = await storageFolder.GetFileAsync("ms-appx:///AppCredentials.json");

            string text = await Windows.Storage.FileIO.ReadTextAsync(file);

            AppCredentials credentials = JsonSerializer.Deserialize<AppCredentials>(text);
            ClientId = credentials.ClientId;
            ClientSecret = credentials.ClientSecret;
        }
    }
}
