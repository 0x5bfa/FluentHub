using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Queries.Repositories
{
	public class CommitQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public CommitQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<PageResult<Commit>> GetPageAsync(
			string owner,
			string name,
			string refs,
			PageRequest page,
			OctokitGraphQLModel.CommitAuthor? author = null,
			string? path = null,
			string? since = null,
			string? until = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

			if (string.IsNullOrEmpty(path))
				path = ".";

			var query = new Query()
				.Repository(name, owner)
				.Ref(refs)
				.Target
				.Cast<OctokitGraphQLModel.Commit>()
				.History(
					page.First,
					page.After,
					page.Last,
					page.Before,
					author,
					path,
					since,
					until)
				.Select(connection => new CommitHistoryConnection
				{
					Edges = connection.Edges.Select(edge => (CommitEdge?)new CommitEdge
					{
						Node = edge.Node.Select(x => new Commit
						{
							AbbreviatedOid = x.AbbreviatedOid,
							Additions = x.Additions,
							ChangedFilesIfAvailable = x.ChangedFilesIfAvailable,
							CommittedDate = x.CommittedDate,
							CommittedDateHumanized = x.CommittedDate.Humanize(null, null),
							Deletions = x.Deletions,
							Message = x.Message,
							MessageHeadline = x.MessageHeadline,
							Oid = x.Oid,

							Author = x.Author.Select(author => new GitActor
							{
								AvatarUrl = author.AvatarUrl(500),

								User = author.User.Select(user => new User
								{
									Login = user.Login,
								}).SingleOrDefault(),
							}).SingleOrDefault(),

							Repository = x.Repository.Select(repo => new Repository
							{
								Name = repo.Name,

								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									Login = owner.Login,
								}).SingleOrDefault(),
							}).SingleOrDefault(),

							Signature = x.Signature.Select(signature => new GitSignature
							{
								IsValid = signature.IsValid,
								Payload = signature.Payload,
								Signature = signature.Payload,
								State = (GitSignatureState)signature.State,
								WasSignedByGitHub = signature.WasSignedByGitHub,

								Signer = signature.Signer.Select(user => new User
								{
									AvatarUrl = user.AvatarUrl(500),
									Login = user.Login
								}).SingleOrDefault(),
							}).SingleOrDefault(),
						}).Single(),
					}).ToList(),

					PageInfo = new()
					{
						EndCursor = connection.PageInfo.EndCursor,
						HasNextPage = connection.PageInfo.HasNextPage,
						HasPreviousPage = connection.PageInfo.HasPreviousPage,
						StartCursor = connection.PageInfo.StartCursor,
					},
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return new PageResult<Commit>(
				response.Edges?
					.Where(x => x?.Node is not null)
					.Select(x => x!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}

		public async Task<Commit> GetLatestAsync(string name, string owner, string refs, string path, CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrEmpty(path))
				path = ".";

			var query = new Query()
				.Repository(name, owner)
				.Ref(refs)
				.Target
				.Cast<OctokitGraphQLModel.Commit>()
				.Select(commit => new Commit
				{
					History = commit.History(1, null, null, null, null, path, null, null).Select(history => new CommitHistoryConnection
					{
						Nodes = history.Nodes.Select(y => (Commit?)new Commit
						{
							AbbreviatedOid = y.AbbreviatedOid,
							Additions = y.Additions,
							ChangedFilesIfAvailable = y.ChangedFilesIfAvailable,
							CommittedDate = y.CommittedDate,
							CommittedDateHumanized = y.CommittedDate.Humanize(null, null),
							Deletions = y.Deletions,
							Message = y.Message,
							MessageHeadline = y.MessageHeadline,
							Oid = y.Oid,

							Author = y.Author.Select(author => new GitActor
							{
								AvatarUrl = author.AvatarUrl(500),
								User = author.User.Select(user => new User
								{
									Login = user.Login,
								})
								.SingleOrDefault(),
							})
							.SingleOrDefault(),
						})
						.ToList(),

						TotalCount = history.TotalCount,
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}
	}
}
