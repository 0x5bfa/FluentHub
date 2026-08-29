using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public class BlobQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public BlobQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<Blob> GetAsync(string name, string owner, string branch, string path, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.Repository(name, owner)
				.Object(expression: branch + ":" + path)
				.Cast<OctokitGraphQLModel.Blob>()
				.Select(x => new Blob
				{
					AbbreviatedOid = x.AbbreviatedOid,
					ByteSize = x.ByteSize,
					CommitUrl = x.CommitUrl,
					Id = x.Id,
					IsBinary = x.IsTruncated,
					IsTruncated = x.IsTruncated,
					Oid = x.Oid,
					Text = x.Text,
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}
	}
}
