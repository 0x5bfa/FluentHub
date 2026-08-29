// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Serialization;

internal sealed class GraphQLResult<T>
{
	[JsonPropertyName("result")]
	public T? Result { get; set; }
}

internal sealed class RepositoryObjectResult<T>
{
	[JsonPropertyName("object")]
	public T? Object { get; set; }
}

internal sealed class ProfileReadmeQueryResult
{
	public bool IsPrivate { get; set; }

	public string? Name { get; set; }

	public NameResult? DefaultBranchRef { get; set; }

	public LoginResult? Owner { get; set; }

	[JsonPropertyName("object")]
	public Blob? Object { get; set; }
}

internal sealed class NameResult
{
	public string? Name { get; set; }
}

internal sealed class LoginResult
{
	public string? Login { get; set; }
}

internal sealed class RepositoryRefResult
{
	public RefTargetResult? Ref { get; set; }
}

internal sealed class RefTargetResult
{
	public Commit? Target { get; set; }
}

internal sealed class GraphQLNodes<T>
{
	public List<T?> Nodes { get; set; } = [];
}

internal sealed class PinnedRepositoriesResult
{
	public GraphQLNodes<Repository> PinnableItems { get; set; } = new();

	public GraphQLNodes<Repository> PinnedItems { get; set; } = new();
}

internal sealed class RepositoryDetailsResult : Repository
{
	public RefConnection? Heads { get; set; }

	public RefConnection? Tags { get; set; }
}

internal sealed class RepositoryBodyResult
{
	public IssueComment? Issue { get; set; }

	public IssueComment? PullRequest { get; set; }
}

[JsonSourceGenerationOptions(
	PropertyNameCaseInsensitive = true,
	UseStringEnumConverter = true)]
[JsonSerializable(typeof(GraphQLResult<AddStarResult>))]
[JsonSerializable(typeof(GraphQLResult<RemoveStarResult>))]
[JsonSerializable(typeof(GraphQLResult<UpdateSubscriptionResult>))]
[JsonSerializable(typeof(GraphQLResult<CreateIssueResult>))]
[JsonSerializable(typeof(GraphQLResult<UpdateIssueResult>))]
[JsonSerializable(typeof(GraphQLResult<CloseIssueResult>))]
[JsonSerializable(typeof(GraphQLResult<ReopenIssueResult>))]
[JsonSerializable(typeof(GraphQLResult<AddCommentResult>))]
[JsonSerializable(typeof(GraphQLResult<UpdateIssueCommentResult>))]
[JsonSerializable(typeof(GraphQLResult<DeleteIssueCommentResult>))]
[JsonSerializable(typeof(GraphQLResult<AddReactionResult>))]
[JsonSerializable(typeof(GraphQLResult<RemoveReactionResult>))]
[JsonSerializable(typeof(GraphQLResult<UpdatePullRequestResult>))]
[JsonSerializable(typeof(GraphQLResult<ClosePullRequestResult>))]
[JsonSerializable(typeof(GraphQLResult<ReopenPullRequestResult>))]
[JsonSerializable(typeof(GraphQLResult<MergePullRequestResult>))]
[JsonSerializable(typeof(GraphQLResult<AddPullRequestReviewResult>))]
[JsonSerializable(typeof(GraphQLResult<User>))]
[JsonSerializable(typeof(GraphQLResult<Organization>))]
[JsonSerializable(typeof(GraphQLResult<Repository>))]
[JsonSerializable(typeof(GraphQLResult<RepositoryObjectResult<Blob>>))]
[JsonSerializable(typeof(GraphQLResult<RepositoryObjectResult<Tree>>))]
[JsonSerializable(typeof(GraphQLResult<ProfileReadmeQueryResult>))]
[JsonSerializable(typeof(GraphQLResult<RepositoryRefResult>))]
[JsonSerializable(typeof(GraphQLResult<PinnedRepositoriesResult>))]
[JsonSerializable(typeof(GraphQLResult<RepositoryDetailsResult>))]
[JsonSerializable(typeof(GraphQLResult<RepositoryBodyResult>))]
[JsonSerializable(typeof(System.Text.Json.JsonElement))]
internal sealed partial class GitHubGraphQLJsonContext : JsonSerializerContext;
