namespace FluentHub.Octokit.Queries.Repositories
{
	public class BlobQueries
	{
		public async Task<Blob> GetAsync(string name, string owner, string branch, string path)
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

			var response = await App.Connection.Run(query);

			return response;
		}
	}
}
