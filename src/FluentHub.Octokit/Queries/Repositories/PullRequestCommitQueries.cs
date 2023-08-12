namespace FluentHub.Octokit.Queries.Repositories
{
	public class PullRequestCommitQueries
	{
		public async Task<List<Commit>> GetAllAsync(string owner, string name, int number)
		{
			var query = new Query()
				.Repository(name, owner)
				.PullRequest(number)
				.Commits(first: 30)
				.Nodes
				.Select(x => x.Commit.Select(y => new Commit
				{
					AbbreviatedOid = y.AbbreviatedOid,
					CommittedDate = y.CommittedDate,
					Message = y.Message,
					MessageHeadline = y.MessageHeadline,
					Oid = y.Oid,

					Author = new()
					{
						AvatarUrl = y.Author.AvatarUrl(500),
						User = y.Author.User.Select(user => new User
						{
							Login = user.Login,
						})
						.SingleOrDefault(),
					},

					Repository = y.Repository.Select(repo => new Repository
					{
						Name = repo.Name,
						Owner = repo.Owner.Select(owner => new RepositoryOwner
						{
							Login = owner.Login,
						})
						.SingleOrDefault(),
					})
					.SingleOrDefault(),
				})
				.Single())
				.Compile();

			var result = await App.Connection.Run(query);

			return result.ToList();
		}
	}
}
