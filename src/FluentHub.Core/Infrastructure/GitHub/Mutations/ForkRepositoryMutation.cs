using FluentHub.Core.Infrastructure.GitHub.Clients;
using System.IO;
using Octokit.Transport;

namespace FluentHub.Core.Infrastructure.GitHub.Mutations
{
	public sealed class ForkRepositoryMutation
	{
		private readonly IGitHubApiClient _gitHub;

		public ForkRepositoryMutation(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public async Task<IReadOnlyList<ForkOwner>> GetAvailableOwnersAsync(
			CancellationToken cancellationToken = default)
		{
			var viewer = await _gitHub.RunRestAsync(
				(client, token) => client.Users.GetAuthenticatedAsync(token),
				cancellationToken);
			var organizations = await _gitHub.RunRestAsync(
				(client, token) => client.Organizations.GetForAuthenticatedAsync(token),
				cancellationToken);

			return new[]
				{
					new ForkOwner
					{
						AvatarUrl = viewer.AvatarUrl ?? string.Empty,
						Login = viewer.Login,
					},
				}
				.Concat(organizations
					.Where(organization => !string.Equals(organization.Login, viewer.Login, StringComparison.OrdinalIgnoreCase))
					.OrderBy(organization => organization.Login, StringComparer.OrdinalIgnoreCase)
					.Select(organization => new ForkOwner
					{
						AvatarUrl = organization.AvatarUrl ?? string.Empty,
						IsOrganization = true,
						Login = organization.Login,
					}))
				.ToList();
		}

		public async Task<CreateForkResult> ExecuteAsync(
			CreateForkRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);
			ArgumentNullException.ThrowIfNull(request.DestinationOwner);
			ArgumentException.ThrowIfNullOrWhiteSpace(request.SourceOwner);
			ArgumentException.ThrowIfNullOrWhiteSpace(request.SourceName);
			ArgumentException.ThrowIfNullOrWhiteSpace(request.DestinationOwner.Login);
			ArgumentException.ThrowIfNullOrWhiteSpace(request.RepositoryName);

			var created = await _gitHub.RunRestAsync(
				(client, token) => client.Repositories.CreateForkAsync(
					request.SourceOwner,
					request.SourceName,
					new OctokitRest.CreateForkOptions
					{
						Organization = request.DestinationOwner.IsOrganization
							? request.DestinationOwner.Login
							: null,
						Name = request.RepositoryName.Trim(),
						DefaultBranchOnly = request.DefaultBranchOnly,
					},
					token),
				cancellationToken);
			var createdOwner = created.Owner?.Login;
			if (string.IsNullOrWhiteSpace(created.Name) ||
				string.IsNullOrWhiteSpace(created.FullName) ||
				string.IsNullOrWhiteSpace(createdOwner))
			{
				throw new InvalidDataException("GitHub returned an incomplete fork response.");
			}

			if (request.Description is not null)
			{
				await UpdateDescriptionAsync(
					createdOwner,
					created.Name,
					request.Description,
					cancellationToken);
			}

			return new CreateForkResult
			{
				FullName = created.FullName,
				Name = created.Name,
				Owner = createdOwner,
			};
		}

		private async Task UpdateDescriptionAsync(
			string owner,
			string name,
			string description,
			CancellationToken cancellationToken)
		{
			for (var attempt = 0; ; attempt++)
			{
				try
				{
					await _gitHub.RunRestAsync(
						(client, token) => client.Repositories.UpdateDescriptionAsync(
							owner,
							name,
							description,
							token),
						cancellationToken);
					return;
				}
				catch (GitHubApiException exception)
					when (exception.StatusCode == System.Net.HttpStatusCode.NotFound && attempt < 3)
				{
					await Task.Delay(TimeSpan.FromSeconds(attempt + 1), cancellationToken);
				}
			}
		}

	}
}
