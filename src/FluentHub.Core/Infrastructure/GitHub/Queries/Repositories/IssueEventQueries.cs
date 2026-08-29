// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;

public sealed class IssueEventQueries
{
	private readonly IGitHubApiClient _gitHub;

	public IssueEventQueries(IGitHubApiClient gitHub)
		=> _gitHub = gitHub;

	public async Task<List<object>> GetAllAsync(
		string owner,
		string name,
		int number,
		CancellationToken cancellationToken = default)
	{
		var response = await _gitHub.RunGraphQLAsync(
			TimelineQueries.IssueOperation,
			GitHubGraphQLJsonContext.Default.JsonElement,
			writer =>
			{
				writer.WriteString("owner", owner);
				writer.WriteString("name", name);
				writer.WriteNumber("number", number);
			},
			cancellationToken);

		return TimelineEventJson.Read(response, "issue");
	}
}
