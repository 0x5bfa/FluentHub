using FluentHub.Octokit.Queries.Repositories;
using Windows.Storage;

namespace FluentHub.App.Services
{
	public class MarkdownApiHandler
	{
		public async Task<string> GetHtmlAsync(string owner, string name, string branch, string theme)
		{
			StorageFile indexFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/WebView/index.html"));
			var indexHtml = await FileIO.ReadTextAsync(indexFile);

			RepositoryQueries queries = new();
			return await queries.GetReadmeHtml(owner, name, branch, theme, indexHtml);
		}

		public async Task<string> GetHtmlAsync(string html, string url, string theme)
		{
			StorageFile indexFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/WebView/index.html"));
			var index = await FileIO.ReadTextAsync(indexFile);

			MarkdownQueries queries = new();
			return queries.GetHtml(index, html, url, theme, true);
		}
	}
}
