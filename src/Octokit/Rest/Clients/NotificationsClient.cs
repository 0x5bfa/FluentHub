// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Octokit.Transport;

namespace Octokit.Rest;

public sealed class NotificationsClient(GitHubHttpClient transport) : GitHubRestClientBase(transport)
{
	public async Task<IReadOnlyList<GitHubNotification>> GetAllAsync(
		NotificationRequest? request = null,
		PageOptions? options = null,
		CancellationToken cancellationToken = default)
	{
		request ??= new NotificationRequest();
		options ??= new PageOptions();
		options.Validate();

		var result = new List<GitHubNotification>();
		for (var page = options.StartPage; page < options.StartPage + options.PageCount; page++)
		{
			var uri = CreateUri(request, page, options.PageSize);
			var items = await Transport.GetAsync(
				uri,
				GitHubRestJsonContext.Default.ListGitHubNotification,
				cancellationToken).ConfigureAwait(false);
			result.AddRange(items);

			if (items.Count < options.PageSize)
				break;
		}

		return result;
	}

	private static string CreateUri(NotificationRequest request, int page, int pageSize)
	{
		var parameters = new List<string>
		{
			$"all={request.All.ToString().ToLowerInvariant()}",
			$"participating={request.Participating.ToString().ToLowerInvariant()}",
			$"per_page={pageSize}",
			$"page={page}",
		};

		if (request.Since is { } since)
			parameters.Add($"since={Uri.EscapeDataString(since.ToUniversalTime().ToString("O"))}");
		if (request.Before is { } before)
			parameters.Add($"before={Uri.EscapeDataString(before.ToUniversalTime().ToString("O"))}");

		return "notifications?" + string.Join('&', parameters);
	}
}
