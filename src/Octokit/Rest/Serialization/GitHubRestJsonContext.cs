// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json.Serialization;

namespace Octokit.Rest;

[JsonSourceGenerationOptions(
	PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
	DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
	GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(GitHubUser))]
[JsonSerializable(typeof(List<GitHubOrganization>))]
[JsonSerializable(typeof(List<GitHubActivityEvent>))]
[JsonSerializable(typeof(List<GitHubNotification>))]
[JsonSerializable(typeof(SearchResponse<GitHubCodeSearchItem>))]
[JsonSerializable(typeof(SearchResponse<GitHubIssueSearchItem>))]
[JsonSerializable(typeof(SearchResponse<GitHubRepository>))]
[JsonSerializable(typeof(SearchResponse<GitHubUser>))]
[JsonSerializable(typeof(GitHubCommit))]
[JsonSerializable(typeof(List<GitHubFileChange>))]
[JsonSerializable(typeof(List<GitReferenceName>))]
[JsonSerializable(typeof(GitHubReadme))]
[JsonSerializable(typeof(CreateForkOptions))]
[JsonSerializable(typeof(UpdateRepositoryRequest))]
[JsonSerializable(typeof(GitHubRepositoryIdentity))]
[JsonSerializable(typeof(List<RepositoryIssueType>))]
internal sealed partial class GitHubRestJsonContext : JsonSerializerContext;
