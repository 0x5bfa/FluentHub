using FluentHub.Core.Infrastructure.GitHub.Clients;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;
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
				client => client.User.Current(),
				cancellationToken);
			var organizations = await _gitHub.RunRestAsync(
				client => client.Organization.GetAllForCurrent(),
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

			var endpoint = $"repos/{Uri.EscapeDataString(request.SourceOwner)}/{Uri.EscapeDataString(request.SourceName)}/forks";
			using var message = new HttpRequestMessage(HttpMethod.Post, endpoint)
			{
				Content = new StringContent(CreateRequestJson(request), Encoding.UTF8, "application/json"),
			};
			message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
			message.Headers.Add("X-GitHub-Api-Version", GitHubHttpClient.RestApiVersion);

			using var response = await _gitHub.SendRestAsync(message, cancellationToken);
			var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
			if (!response.IsSuccessStatusCode)
			{
				throw new HttpRequestException(
					GetErrorMessage(responseJson) ?? $"GitHub returned {(int)response.StatusCode} ({response.ReasonPhrase}).",
					null,
					response.StatusCode);
			}

			using var responseDocument = JsonDocument.Parse(responseJson);
			var responseRoot = responseDocument.RootElement;
			var createdName = GetRequiredString(responseRoot, "name");
			var createdFullName = GetRequiredString(responseRoot, "full_name");
			if (!responseRoot.TryGetProperty("owner", out var createdOwnerElement))
			{
				throw new InvalidDataException("GitHub returned an incomplete fork response.");
			}
			var createdOwner = GetRequiredString(createdOwnerElement, "login");

			if (request.Description is not null)
			{
				await UpdateDescriptionAsync(
					createdOwner,
					createdName,
					request.Description,
					cancellationToken);
			}

			return new CreateForkResult
			{
				FullName = createdFullName,
				Name = createdName,
				Owner = createdOwner,
			};
		}

		private static string CreateRequestJson(CreateForkRequest request)
		{
			using var stream = new MemoryStream();
			using (var writer = new Utf8JsonWriter(stream))
			{
				writer.WriteStartObject();
				if (request.DestinationOwner.IsOrganization)
					writer.WriteString("organization", request.DestinationOwner.Login);
				writer.WriteString("name", request.RepositoryName.Trim());
				writer.WriteBoolean("default_branch_only", request.DefaultBranchOnly);
				writer.WriteEndObject();
			}

			return Encoding.UTF8.GetString(stream.ToArray());
		}

		private static string? GetErrorMessage(string responseJson)
		{
			try
			{
				using var document = JsonDocument.Parse(responseJson);
				var root = document.RootElement;
				var message = root.TryGetProperty("message", out var messageElement)
					? messageElement.GetString()
					: null;

				if (!root.TryGetProperty("errors", out var errors) || errors.ValueKind != JsonValueKind.Array)
					return message;

				var details = errors.EnumerateArray()
					.Select(error =>
					{
						if (error.TryGetProperty("message", out var detailMessage))
							return detailMessage.GetString();

						var field = error.TryGetProperty("field", out var fieldElement)
							? fieldElement.GetString()
							: null;
						var code = error.TryGetProperty("code", out var codeElement)
							? codeElement.GetString()
							: null;
						return field is not null && code is not null ? $"{field}: {code}" : code;
					})
					.Where(detail => !string.IsNullOrWhiteSpace(detail))
					.ToList();

				return details.Count == 0
					? message
					: $"{message ?? "GitHub rejected the fork request"}: {string.Join("; ", details)}";
			}
			catch (System.Text.Json.JsonException)
			{
				return null;
			}
		}

		private static string GetRequiredString(JsonElement element, string propertyName)
		{
			if (element.TryGetProperty(propertyName, out var property) &&
				property.GetString() is { Length: > 0 } value)
			{
				return value;
			}

			throw new InvalidDataException("GitHub returned an incomplete fork response.");
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
						client => client.Repository.Edit(
							owner,
							name,
							new OctokitV3.RepositoryUpdate { Description = description }),
						cancellationToken);
					return;
				}
				catch (OctokitV3.NotFoundException) when (attempt < 3)
				{
					await Task.Delay(TimeSpan.FromSeconds(attempt + 1), cancellationToken);
				}
			}
		}

	}
}
