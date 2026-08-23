using FluentHub.Core.Clients;

namespace FluentHub.Core.Mutations
{
	public sealed class ForkRepositoryMutation
	{
		private readonly IGitHubApiClient _gitHub;

		public ForkRepositoryMutation(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<OctokitV3.Repository> ExecuteAsync(
			string owner,
			string name,
			CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(owner);
			ArgumentException.ThrowIfNullOrWhiteSpace(name);

			return _gitHub.RunRestAsync(
				client => client.Repository.Forks.Create(
					owner,
					name,
					new OctokitV3.NewRepositoryFork()),
				cancellationToken);
		}
	}
}
