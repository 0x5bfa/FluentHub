// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using System.Text.Json;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public class TreeQueries
	{
		private const string TreeQuery = """
			query Tree($owner: String!, $name: String!, $expression: String!) {
			  result: repository(owner: $owner, name: $name) {
			    object(expression: $expression) {
			      ... on Tree { entries { name path type } }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public TreeQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public async Task<List<TreeEntry>> GetAllAsync(
			string name,
			string owner,
			string refs,
			string path,
			CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				TreeQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepositoryObjectResultTree,
				writer =>
				{
					writer.WriteString("owner", owner);
					writer.WriteString("name", name);
					writer.WriteString("expression", $"{refs}:{path}");
				},
				cancellationToken);
			return response.Result?.Object?.Entries ?? [];
		}

		public async Task<(List<TreeEntry> Files, List<Commit> Commits)> GetWithObjectNameAsync(
			string name,
			string owner,
			string refs,
			string path,
			CancellationToken cancellationToken = default)
		{
			var files = await GetAllAsync(name, owner, refs, path, cancellationToken);
			if (files.Count == 0)
				return (files, []);

			var query = BuildCommitQuery(files.Count);
			var data = await _gitHub.RunGraphQLAsync(
				query,
				GitHubGraphQLJsonContext.Default.JsonElement,
				writer =>
				{
					writer.WriteString("owner", owner);
					writer.WriteString("name", name);
					writer.WriteString("ref", refs);
					for (var index = 0; index < files.Count; index++)
						writer.WriteString($"path{index}", files[index].Path ?? files[index].Name);
				},
				cancellationToken);

			var commits = ReadCommits(data, files.Count);
			return (files, commits);
		}

		private static string BuildCommitQuery(int count)
		{
			var builder = new StringBuilder("query FileCommits($owner: String!, $name: String!, $ref: String!");
			for (var index = 0; index < count; index++)
				builder.Append(", $path").Append(index).Append(": String!");
			builder.Append(") { result: repository(owner: $owner, name: $name) { ref(qualifiedName: $ref) { target { ... on Commit {");
			for (var index = 0; index < count; index++)
			{
				builder.Append(" history").Append(index).Append(": history(first: 1, path: $path").Append(index)
					.Append(") { nodes { message committedDate } }");
			}
			builder.Append(" } } } } }");
			return builder.ToString();
		}

		private static List<Commit> ReadCommits(JsonElement data, int count)
		{
			if (!TryGet(data, "result", out var repository) ||
				!TryGet(repository, "ref", out var reference) ||
				!TryGet(reference, "target", out var target))
			{
				return [];
			}

			var commits = new List<Commit>();
			for (var index = 0; index < count; index++)
			{
				if (!TryGet(target, $"history{index}", out var history) ||
					!TryGet(history, "nodes", out var nodes) ||
					nodes.ValueKind != JsonValueKind.Array ||
					nodes.GetArrayLength() == 0)
				{
					continue;
				}

				var node = nodes[0];
				var committedDate = TryGet(node, "committedDate", out var dateElement) && dateElement.TryGetDateTimeOffset(out var date)
					? date
					: default;
				commits.Add(new Commit
				{
					Message = TryGet(node, "message", out var message) ? message.GetString() ?? string.Empty : string.Empty,
					CommittedDate = committedDate,
					CommittedDateHumanized = committedDate.ToRelativeTime(),
				});
			}
			return commits;
		}

		private static bool TryGet(JsonElement element, string name, out JsonElement value)
		{
			value = default;
			return element.ValueKind == JsonValueKind.Object && element.TryGetProperty(name, out value);
		}
	}
}
