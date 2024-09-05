using GraphQL;
using Newtonsoft.Json.Linq;

namespace FluentHub.Octokit.Queries.Repositories
{
	public class TreeQueries
	{
		public async Task<List<TreeEntry>> GetAllAsync(string name, string owner, string refs, string path)
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

			var response = await App.Connection.Run(query);

			return response.ToList();
		}

		public async Task<(List<TreeEntry> Files, List<Commit> Commits)> GetWithObjectNameAsync(string name, string owner, string refs, string path)
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

			var response1 = await App.Connection.Run(queryToGetFileInfo);

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

			var response2 = await App.GraphQLHttpClient.SendQueryAsync<object>(request2);
			List<Commit> zippedData = new();

			var json = response2.Data as JToken;
			var errors = json["errors"];

			if (errors is not null)
			{
				(List<TreeEntry> Files, List<Commit> Commits) pre = (response1.ToList(), zippedData);

				return pre;
			}

			var target = json
				.Children<JProperty>().FirstOrDefault(x => x.Name == "repository").Value
				.Children<JProperty>().FirstOrDefault(x => x.Name == "ref").Value
				.Children<JProperty>().FirstOrDefault(x => x.Name == "target").Value;

			for (int index = 0; index < response1.ToList().Count; index++)
			{
				var history = target.Children<JProperty>().FirstOrDefault(x => x.Name == $"history{index}").Value;

				var item = history.Children<JProperty>().FirstOrDefault(x => x.Name == "nodes").Value.Children().FirstOrDefault();

				var properties = item.Children<JProperty>();
				var message = properties.FirstOrDefault(x => x.Name == "message").Value.ToString();
				var committedDate = properties.FirstOrDefault(x => x.Name == "committedDate").Value.ToString();

				zippedData.Add(new()
				{
					Message = message,
					CommittedDate = DateTimeOffset.Parse(committedDate),
					CommittedDateHumanized = DateTimeOffset.Parse(committedDate).Humanize()
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
