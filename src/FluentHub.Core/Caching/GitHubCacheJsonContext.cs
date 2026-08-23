// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json.Serialization;
using SystemTextJsonException = System.Text.Json.JsonException;
using SystemTextJsonSerializer = System.Text.Json.JsonSerializer;

namespace FluentHub.Core.Caching
{
	[JsonSourceGenerationOptions(
		GenerationMode = JsonSourceGenerationMode.Metadata,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonSerializable(typeof(CachedUserSummary))]
	[JsonSerializable(typeof(CachedRepositorySummary))]
	[JsonSerializable(typeof(CachedOrganizationSummary))]
	internal sealed partial class GitHubCacheJsonContext : JsonSerializerContext
	{
	}

	internal static class GitHubCacheSerializers
	{
		public static CacheSerializer<User> User { get; } = new(
			static value => SystemTextJsonSerializer.SerializeToUtf8Bytes(
				CachedUserSummary.FromContract(value),
				GitHubCacheJsonContext.Default.CachedUserSummary),
			static bytes => SystemTextJsonSerializer.Deserialize(bytes, GitHubCacheJsonContext.Default.CachedUserSummary)?.ToContract()
				?? throw new SystemTextJsonException("Cached user was null."));

		public static CacheSerializer<Repository> Repository { get; } = new(
			static value => SystemTextJsonSerializer.SerializeToUtf8Bytes(
				CachedRepositorySummary.FromContract(value),
				GitHubCacheJsonContext.Default.CachedRepositorySummary),
			static bytes => SystemTextJsonSerializer.Deserialize(bytes, GitHubCacheJsonContext.Default.CachedRepositorySummary)?.ToContract()
				?? throw new SystemTextJsonException("Cached repository was null."));

		public static CacheSerializer<Organization> Organization { get; } = new(
			static value => SystemTextJsonSerializer.SerializeToUtf8Bytes(
				CachedOrganizationSummary.FromContract(value),
				GitHubCacheJsonContext.Default.CachedOrganizationSummary),
			static bytes => SystemTextJsonSerializer.Deserialize(bytes, GitHubCacheJsonContext.Default.CachedOrganizationSummary)?.ToContract()
				?? throw new SystemTextJsonException("Cached organization was null."));
	}

	internal sealed class CachedUserSummary
	{
		public string AvatarUrl { get; set; } = string.Empty;
		public string? Bio { get; set; }
		public string? Company { get; set; }
		public string Email { get; set; } = string.Empty;
		public bool IsBountyHunter { get; set; }
		public bool IsCampusExpert { get; set; }
		public bool IsDeveloperProgramMember { get; set; }
		public bool IsEmployee { get; set; }
		public bool IsGitHubStar { get; set; }
		public bool IsViewer { get; set; }
		public string? Location { get; set; }
		public string Login { get; set; } = string.Empty;
		public string? Name { get; set; }
		public string? TwitterUsername { get; set; }
		public bool ViewerIsFollowing { get; set; }
		public string? WebsiteUrl { get; set; }
		public int? FollowersCount { get; set; }
		public int? FollowingCount { get; set; }
		public CachedUserStatus? Status { get; set; }

		public static CachedUserSummary FromContract(User value)
			=> new()
			{
				AvatarUrl = value.AvatarUrl,
				Bio = value.Bio,
				Company = value.Company,
				Email = value.Email,
				IsBountyHunter = value.IsBountyHunter,
				IsCampusExpert = value.IsCampusExpert,
				IsDeveloperProgramMember = value.IsDeveloperProgramMember,
				IsEmployee = value.IsEmployee,
				IsGitHubStar = value.IsGitHubStar,
				IsViewer = value.IsViewer,
				Location = value.Location,
				Login = value.Login,
				Name = value.Name,
				TwitterUsername = value.TwitterUsername,
				ViewerIsFollowing = value.ViewerIsFollowing,
				WebsiteUrl = value.WebsiteUrl,
				FollowersCount = value.Followers?.TotalCount,
				FollowingCount = value.Following?.TotalCount,
				Status = CachedUserStatus.FromContract(value.Status),
			};

		public User ToContract()
			=> new()
			{
				AvatarUrl = AvatarUrl,
				Bio = Bio,
				Company = Company,
				Email = Email,
				IsBountyHunter = IsBountyHunter,
				IsCampusExpert = IsCampusExpert,
				IsDeveloperProgramMember = IsDeveloperProgramMember,
				IsEmployee = IsEmployee,
				IsGitHubStar = IsGitHubStar,
				IsViewer = IsViewer,
				Location = Location,
				Login = Login,
				Name = Name,
				TwitterUsername = TwitterUsername,
				ViewerIsFollowing = ViewerIsFollowing,
				WebsiteUrl = WebsiteUrl,
				Followers = FollowersCount is int followers
					? new FollowerConnection { TotalCount = followers }
					: null!,
				Following = FollowingCount is int following
					? new FollowingConnection { TotalCount = following }
					: null!,
				Status = Status?.ToContract(),
			};
	}

	internal sealed class CachedUserStatus
	{
		public string? Emoji { get; set; }
		public string? Message { get; set; }
		public bool IndicatesLimitedAvailability { get; set; }

		public static CachedUserStatus? FromContract(UserStatus? value)
			=> value is null
				? null
				: new()
				{
					Emoji = value.Emoji,
					Message = value.Message,
					IndicatesLimitedAvailability = value.IndicatesLimitedAvailability,
				};

		public UserStatus ToContract()
			=> new()
			{
				Emoji = Emoji,
				Message = Message,
				IndicatesLimitedAvailability = IndicatesLimitedAvailability,
			};
	}

	internal sealed class CachedOrganizationSummary
	{
		public string AvatarUrl { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string? Email { get; set; }
		public string? Id { get; set; }
		public bool IsVerified { get; set; }
		public string? Location { get; set; }
		public string Login { get; set; } = string.Empty;
		public string? Name { get; set; }
		public string? TwitterUsername { get; set; }
		public string Url { get; set; } = string.Empty;
		public bool ViewerCanChangePinnedItems { get; set; }
		public bool ViewerCanSponsor { get; set; }
		public bool ViewerIsAMember { get; set; }
		public bool ViewerIsFollowing { get; set; }
		public bool ViewerIsSponsoring { get; set; }
		public string? WebsiteUrl { get; set; }

		public static CachedOrganizationSummary FromContract(Organization value)
			=> new()
			{
				AvatarUrl = value.AvatarUrl,
				Description = value.Description,
				Email = value.Email,
				Id = value.Id.Value,
				IsVerified = value.IsVerified,
				Location = value.Location,
				Login = value.Login,
				Name = value.Name,
				TwitterUsername = value.TwitterUsername,
				Url = value.Url,
				ViewerCanChangePinnedItems = value.ViewerCanChangePinnedItems,
				ViewerCanSponsor = value.ViewerCanSponsor,
				ViewerIsAMember = value.ViewerIsAMember,
				ViewerIsFollowing = value.ViewerIsFollowing,
				ViewerIsSponsoring = value.ViewerIsSponsoring,
				WebsiteUrl = value.WebsiteUrl,
			};

		public Organization ToContract()
			=> new()
			{
				AvatarUrl = AvatarUrl,
				Description = Description,
				Email = Email,
				Id = CacheContractMapper.ToId(Id),
				IsVerified = IsVerified,
				Location = Location,
				Login = Login,
				Name = Name,
				TwitterUsername = TwitterUsername,
				Url = Url,
				ViewerCanChangePinnedItems = ViewerCanChangePinnedItems,
				ViewerCanSponsor = ViewerCanSponsor,
				ViewerIsAMember = ViewerIsAMember,
				ViewerIsFollowing = ViewerIsFollowing,
				ViewerIsSponsoring = ViewerIsSponsoring,
				WebsiteUrl = WebsiteUrl,
			};
	}

	internal sealed class CachedRepositorySummary
	{
		public string? Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string? HomepageUrl { get; set; }
		public int StargazerCount { get; set; }
		public int ForkCount { get; set; }
		public bool ForkingAllowed { get; set; }
		public bool HasIssuesEnabled { get; set; }
		public bool HasProjectsEnabled { get; set; }
		public bool IsArchived { get; set; }
		public bool IsEmpty { get; set; }
		public bool IsFork { get; set; }
		public bool IsInOrganization { get; set; }
		public bool IsPrivate { get; set; }
		public bool IsTemplate { get; set; }
		public bool ViewerHasStarred { get; set; }
		public RepositoryPermission? ViewerPermission { get; set; }
		public SubscriptionState? ViewerSubscription { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }
		public string? LicenseName { get; set; }
		public string? DefaultBranchName { get; set; }
		public int? WatchersCount { get; set; }
		public int? ReleasesCount { get; set; }
		public int? IssuesCount { get; set; }
		public int? PullRequestsCount { get; set; }
		public CachedRepositoryOwner? Owner { get; set; }
		public CachedRelease? LatestRelease { get; set; }
		public CachedLanguage? PrimaryLanguage { get; set; }
		public List<CachedLanguage?>? Languages { get; set; }

		public static CachedRepositorySummary FromContract(Repository value)
			=> new()
			{
				Id = value.Id.Value,
				Name = value.Name,
				Description = value.Description,
				HomepageUrl = value.HomepageUrl,
				StargazerCount = value.StargazerCount,
				ForkCount = value.ForkCount,
				ForkingAllowed = value.ForkingAllowed,
				HasIssuesEnabled = value.HasIssuesEnabled,
				HasProjectsEnabled = value.HasProjectsEnabled,
				IsArchived = value.IsArchived,
				IsEmpty = value.IsEmpty,
				IsFork = value.IsFork,
				IsInOrganization = value.IsInOrganization,
				IsPrivate = value.IsPrivate,
				IsTemplate = value.IsTemplate,
				ViewerHasStarred = value.ViewerHasStarred,
				ViewerPermission = value.ViewerPermission,
				ViewerSubscription = value.ViewerSubscription,
				UpdatedAt = value.UpdatedAt,
				LicenseName = value.LicenseInfo?.Name,
				DefaultBranchName = value.DefaultBranchRef?.Name,
				WatchersCount = value.Watchers?.TotalCount,
				ReleasesCount = value.Releases?.TotalCount,
				IssuesCount = value.Issues?.TotalCount,
				PullRequestsCount = value.PullRequests?.TotalCount,
				Owner = CachedRepositoryOwner.FromContract(value.Owner),
				LatestRelease = CachedRelease.FromContract(value.LatestRelease),
				PrimaryLanguage = CachedLanguage.FromContract(value.PrimaryLanguage),
				Languages = value.Languages?.Nodes?
					.Select(CachedLanguage.FromContract)
					.ToList(),
			};

		public Repository ToContract()
			=> new()
			{
				Id = CacheContractMapper.ToId(Id),
				Name = Name,
				Description = Description,
				HomepageUrl = HomepageUrl,
				StargazerCount = StargazerCount,
				ForkCount = ForkCount,
				ForkingAllowed = ForkingAllowed,
				HasIssuesEnabled = HasIssuesEnabled,
				HasProjectsEnabled = HasProjectsEnabled,
				IsArchived = IsArchived,
				IsEmpty = IsEmpty,
				IsFork = IsFork,
				IsInOrganization = IsInOrganization,
				IsPrivate = IsPrivate,
				IsTemplate = IsTemplate,
				ViewerHasStarred = ViewerHasStarred,
				ViewerPermission = ViewerPermission,
				ViewerSubscription = ViewerSubscription,
				UpdatedAt = UpdatedAt,
				LicenseInfo = LicenseName is null ? null : new License { Name = LicenseName },
				DefaultBranchRef = DefaultBranchName is null ? null : new Ref { Name = DefaultBranchName },
				Watchers = WatchersCount is int watchers
					? new UserConnection { TotalCount = watchers }
					: null!,
				Releases = ReleasesCount is int releases
					? new ReleaseConnection { TotalCount = releases }
					: null!,
				Issues = IssuesCount is int issues
					? new IssueConnection { TotalCount = issues }
					: null!,
				PullRequests = PullRequestsCount is int pullRequests
					? new PullRequestConnection { TotalCount = pullRequests }
					: null!,
				Owner = Owner?.ToContract() ?? null!,
				LatestRelease = LatestRelease?.ToContract(),
				PrimaryLanguage = PrimaryLanguage?.ToContract(),
				Languages = Languages is null
					? null
					: new LanguageConnection
					{
						Nodes = Languages.Select(static language => language?.ToContract()).ToList(),
					},
			};
	}

	internal sealed class CachedRepositoryOwner
	{
		public string AvatarUrl { get; set; } = string.Empty;
		public string? Id { get; set; }
		public string Login { get; set; } = string.Empty;

		public static CachedRepositoryOwner? FromContract(IRepositoryOwner? value)
			=> value is null
				? null
				: new()
				{
					AvatarUrl = value.AvatarUrl,
					Id = value.Id.Value,
					Login = value.Login,
				};

		public RepositoryOwner ToContract()
			=> new()
			{
				AvatarUrl = AvatarUrl,
				Id = CacheContractMapper.ToId(Id),
				Login = Login,
			};
	}

	internal sealed class CachedRelease
	{
		public string? Description { get; set; }
		public string? DescriptionHTML { get; set; }
		public bool IsDraft { get; set; }
		public bool IsLatest { get; set; }
		public bool IsPrerelease { get; set; }
		public string? Name { get; set; }
		public DateTimeOffset? PublishedAt { get; set; }
		public string? AuthorLogin { get; set; }
		public string? AuthorAvatarUrl { get; set; }

		public static CachedRelease? FromContract(Release? value)
			=> value is null
				? null
				: new()
				{
					Description = value.Description,
					DescriptionHTML = value.DescriptionHTML,
					IsDraft = value.IsDraft,
					IsLatest = value.IsLatest,
					IsPrerelease = value.IsPrerelease,
					Name = value.Name,
					PublishedAt = value.PublishedAt,
					AuthorLogin = value.Author?.Login,
					AuthorAvatarUrl = value.Author?.AvatarUrl,
				};

		public Release ToContract()
			=> new()
			{
				Description = Description,
				DescriptionHTML = DescriptionHTML,
				IsDraft = IsDraft,
				IsLatest = IsLatest,
				IsPrerelease = IsPrerelease,
				Name = Name,
				PublishedAt = PublishedAt,
				Author = AuthorLogin is null && AuthorAvatarUrl is null
					? null
					: new User
					{
						Login = AuthorLogin ?? string.Empty,
						AvatarUrl = AuthorAvatarUrl ?? string.Empty,
					},
			};
	}

	internal sealed class CachedLanguage
	{
		public string? Color { get; set; }
		public string Name { get; set; } = string.Empty;

		public static CachedLanguage? FromContract(Language? value)
			=> value is null
				? null
				: new() { Color = value.Color, Name = value.Name };

		public Language ToContract()
			=> new() { Color = Color, Name = Name };
	}

	internal static class CacheContractMapper
	{
		public static global::Octokit.GraphQL.ID ToId(string? value)
			=> value is null ? default : new(value);
	}
}
