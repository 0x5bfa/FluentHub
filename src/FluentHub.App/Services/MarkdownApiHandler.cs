using FluentHub.Octokit.Queries.Repositories;
using Windows.Storage;

namespace FluentHub.App.Services
{
	public class MarkdownApiHandler
	{
		private readonly IFluentHubGitHubClient _gitHub;

		public MarkdownApiHandler(IFluentHubGitHubClient gitHub)
			=> _gitHub = gitHub;


		public async Task<string> GetHtmlAsync(
			string owner,
			string name,
			string branch,
			string theme,
			CancellationToken cancellationToken = default)
		{
			StorageFile indexFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/WebView/index.html"));
			var indexHtml = await FileIO.ReadTextAsync(indexFile);

			var queries = _gitHub.Repositories.Repositories;
			return await queries.GetReadmeHtmlAsync(owner, name, branch, theme, indexHtml, cancellationToken) ?? string.Empty;
		}

		public async Task<string> GetHtmlAsync(
			string html,
			string url,
			string theme,
			CancellationToken cancellationToken = default)
		{
			StorageFile indexFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/WebView/index.html"));
			var index = await FileIO.ReadTextAsync(indexFile);

			var queries = _gitHub.Repositories.Markdown;
			return await queries.GetHtmlAsync(index, html, url, theme, true, cancellationToken);
		}
	}
}
