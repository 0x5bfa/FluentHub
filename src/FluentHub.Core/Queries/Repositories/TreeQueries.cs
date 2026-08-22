using GraphQL;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;

using FluentHub.Core.Clients;

namespace FluentHub.Core.Queries.Repositories
{
	public class TreeQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public TreeQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<TreeEntry>> GetAllAsync(string name, string owner, string refs, string path, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.Repository(name, owner)
				.Object(expression: refs + ":" + path)
				.Cast<OctokitGraphQLModel.Tree>()
				.Entries
				.Select(x => new TreeEntry
				{
					Name = x.Name,
					Path = x.Path,
					Type = x.Type,
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response.ToList();
		}

		public async Task<(List<TreeEntry> Files, List<Commit> Commits)> GetWithObjectNameAsync(string name, string owner, string refs, string path, CancellationToken cancellationToken = default)
		{
			var queryToGetFileInfo = new Query()
				.Repository(name, owner)
				.Object(expression: refs + ":" + path)
				.Cast<OctokitGraphQLModel.Tree>()
				.Entries
				.Select(x => new TreeEntry
				{
					Name = x.Name,
					Path = x.Path,
					Type = x.Type,
				})
				.Compile();

			var response1 = await _gitHub.RunGraphQLAsync(queryToGetFileInfo, cancellationToken);

			List<Commit> items = new();

			var fragments = GetCommitFragmentList(response1.ToList());

			var request2 = new GraphQLRequest
			{
				Query = @$"
query {{
	repository(name: ""{name}"", owner: ""{owner}"") {{
		ref (qualifiedName: ""{refs}"") {{
			target {{
				... on Commit {{
					{fragments}
				}}
			}}
		}}
	}}
}}",
			};

			var response2 = await _gitHub.SendGraphQLAsync<object>(request2, cancellationToken);
			List<Commit> zippedData = new();
			(List<TreeEntry> Files, List<Commit> Commits) pre = (response1.ToList(), zippedData);

			var json = response2.Data as JToken;
			if (json is null)
				return pre;

			var errors = json["errors"];

			if (errors is not null)
			{
				return pre;
			}

			var target = json
				.Children<JProperty>().FirstOrDefault(x => x.Name == "repository")?.Value
				.Children<JProperty>().FirstOrDefault(x => x.Name == "ref")?.Value
				.Children<JProperty>().FirstOrDefault(x => x.Name == "target")?.Value;

			if (target is null)
				return pre;

			for (int index = 0; index < response1.ToList().Count; index++)
			{
				var history = target.Children<JProperty>().FirstOrDefault(x => x.Name == $"history{index}")?.Value;

				var item = history?.Children<JProperty>().FirstOrDefault(x => x.Name == "nodes")?.Value.Children().FirstOrDefault();
				if (item is null)
					continue;

				var properties = item.Children<JProperty>();
				var message = properties.FirstOrDefault(x => x.Name == "message")?.Value.ToString() ?? string.Empty;
				var committedDate = properties.FirstOrDefault(x => x.Name == "committedDate")?.Value.ToString();
				DateTimeOffset.TryParse(committedDate, out var parsedCommittedDate);

				zippedData.Add(new()
				{
					Message = message,
					CommittedDate = parsedCommittedDate,
					CommittedDateHumanized = parsedCommittedDate.ToRelativeTime()
				});
			}

			(List<TreeEntry> Files, List<Commit> Commits) results = (response1.ToList(), zippedData);

			return results;
		}

		private string GetCommitFragmentList(List<TreeEntry> files)
		{
			string fragments = "";

			for (int index = 0; index < files.Count; index++)
			{
				fragments += @$"
history{index}: history(first: 1, path: ""{files.ElementAt(index).Path}"") {{
	nodes {{
		message
		committedDate
	}}
}}
";
			}

			return fragments;
		}
	}
}
