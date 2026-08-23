using FluentHub.Core.Clients;
using FluentHub.Core.Caching;

namespace FluentHub.Core.Queries.Repositories
{
	public class RepositoryQueries
	{
		private const string RepositoryDetailsCacheCategory = "repository-details-v2";

		private readonly IGitHubApiClient _gitHub;
		private readonly ICacheService? _cache;

		public RepositoryQueries(IGitHubApiClient gitHub, ICacheService? cache = null)
		{
			_gitHub = gitHub;
			_cache = cache;
		}

		public Task<Repository> GetAsync(string owner, string name, CancellationToken cancellationToken = default)
		{
			ValidateRepository(owner, name);
			if (_cache is null)
				return GetUncachedAsync(owner, name, cancellationToken);

			return _cache.GetOrCreateAsync(
				CreateRepositoryKey("repositories", owner, name),
				CachePolicies.Repository,
				GitHubCacheSerializers.Repository,
				token => GetUncachedAsync(owner, name, token),
				cancellationToken);
		}

		private async Task<Repository> GetUncachedAsync(string owner, string name, CancellationToken cancellationToken)
		{
			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.IssueState>> issueState =
				new(new OctokitGraphQLModel.IssueState[] {
					OctokitGraphQLModel.IssueState.Open
				});
			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.PullRequestState>> pullRequestState =
				new(new OctokitGraphQLModel.PullRequestState[] {
					OctokitGraphQLModel.PullRequestState.Open
				});

			var query = new Query()
				.Repository(name, owner)
				.Select(x => new Repository
				{
					Name = x.Name,
					Description = x.Description,
					StargazerCount = x.StargazerCount,
					ForkCount = x.ForkCount,
					IsFork = x.IsFork,
					IsInOrganization = x.IsInOrganization,
					ViewerHasStarred = x.ViewerHasStarred,
					UpdatedAt = x.UpdatedAt,

					LicenseInfo = x.LicenseInfo.Select(licenseInfo => new License
					{
						Name = licenseInfo.Name,
					})
					.SingleOrDefault(),

					Issues = x.Issues(null, null, null, null, null, null, null, issueState).Select(issues => new IssueConnection
					{
						TotalCount = issues.TotalCount
					})
					.Single(),

					PullRequests = x.PullRequests(null, null, null, null, null, null, null, null, pullRequestState).Select(issues => new PullRequestConnection
					{
						TotalCount = issues.TotalCount
					})
					.Single(),

					Owner = x.Owner.Select(owner => new RepositoryOwner
					{
						AvatarUrl = owner.AvatarUrl(500),
						Id = owner.Id,
						Login = owner.Login,
					})
					.Single(),

					PrimaryLanguage = x.PrimaryLanguage.Select(y => new Language
					{
						Name = y.Name,
						Color = y.Color,
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}

		public Task<Repository> GetDetailsAsync(string owner, string name, CancellationToken cancellationToken = default)
		{
			ValidateRepository(owner, name);
			if (_cache is null)
				return GetDetailsUncachedAsync(owner, name, cancellationToken);

			return _cache.GetOrCreateAsync(
				CreateRepositoryKey(RepositoryDetailsCacheCategory, owner, name),
				CachePolicies.Repository,
				GitHubCacheSerializers.Repository,
				token => GetDetailsUncachedAsync(owner, name, token),
				cancellationToken);
		}

		public Task InvalidateAsync(string owner, string name, CancellationToken cancellationToken = default)
		{
			ValidateRepository(owner, name);
			if (_cache is null)
				return Task.CompletedTask;

			return Task.WhenAll(
				_cache.RemoveAsync(CreateRepositoryKey("repositories", owner, name), cancellationToken),
				_cache.RemoveAsync(CreateRepositoryKey("repository-details", owner, name), cancellationToken),
				_cache.RemoveAsync(CreateRepositoryKey(RepositoryDetailsCacheCategory, owner, name), cancellationToken));
		}

		private async Task<Repository> GetDetailsUncachedAsync(string owner, string name, CancellationToken cancellationToken)
		{
			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.IssueState>> issueState =
				new(new OctokitGraphQLModel.IssueState[] {
					OctokitGraphQLModel.IssueState.Open
				});
			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.PullRequestState>> pullRequestState =
				new(new OctokitGraphQLModel.PullRequestState[] {
					OctokitGraphQLModel.PullRequestState.Open
				});

			var query = new Query()
				.Repository(owner: owner, name: name)
				.Select(x => new Repository
				{
					Id = x.Id,
					HomepageUrl = x.HomepageUrl,
					ForkingAllowed = x.ForkingAllowed,
					HasIssuesEnabled = x.HasIssuesEnabled,
					HasProjectsEnabled = x.HasProjectsEnabled,
					IsArchived = x.IsArchived,
					IsEmpty = x.IsEmpty,
					IsPrivate = x.IsPrivate,
					IsTemplate = x.IsTemplate,
					ViewerSubscription = (SubscriptionState?)x.ViewerSubscription,
					Name = x.Name,
					Description = x.Description,
					StargazerCount = x.StargazerCount,
					ForkCount = x.ForkCount,
					IsFork = x.IsFork,
					IsInOrganization = x.IsInOrganization,
					ViewerHasStarred = x.ViewerHasStarred,
					ViewerPermission = x.ViewerPermission == null
						? null
						: (RepositoryPermission?)x.ViewerPermission.Value,
					UpdatedAt = x.UpdatedAt,

					LicenseInfo = x.LicenseInfo.Select(licenseInfo => new License
					{
						Name = licenseInfo.Name,
					})
					.SingleOrDefault(),

					DefaultBranchRef = x.DefaultBranchRef.Select(defaultbranchref => new Ref
					{
						Name = defaultbranchref.Name,
					})
					.SingleOrDefault(),

					Watchers = x.Watchers(null, null, null, null).Select(watchers => new UserConnection
					{
						TotalCount = watchers.TotalCount,
					})
					.Single(),

					Releases = x.Releases(null, null, null, null, null).Select(releases => new ReleaseConnection
					{
						TotalCount = releases.TotalCount,
					})
					.Single(),

					Issues = x.Issues(null, null, null, null, null, null, null, issueState).Select(issues => new IssueConnection
					{
						TotalCount = issues.TotalCount
					})
					.Single(),

					PullRequests = x.PullRequests(null, null, null, null, null, null, null, null, pullRequestState).Select(issues => new PullRequestConnection
					{
						TotalCount = issues.TotalCount
					})
					.Single(),

					Owner = x.Owner.Select(owner => new RepositoryOwner
					{
						AvatarUrl = owner.AvatarUrl(500),
						Id = owner.Id,
						Login = owner.Login,
					})
					.Single(),

					LatestRelease = x.LatestRelease.Select(release => new Release
					{
						Description = release.Description,
						DescriptionHTML = release.DescriptionHTML,
						IsDraft = release.IsDraft,
						IsLatest = release.IsLatest,
						IsPrerelease = release.IsPrerelease,
						Name = release.Name,
						PublishedAt = release.PublishedAt,
						PublishedAtHumanized = release.PublishedAt.ToRelativeTime(),

						Author = release.Author.Select(author => new User
						{
							Login = author.Login,
							AvatarUrl = author.AvatarUrl(500),
						})
						.Single(),
					})
					.SingleOrDefault(),

					Languages = x.Languages(10, null, null, null, null).Select(langConection => new LanguageConnection
					{
						Nodes = langConection.Nodes.Select(lang => (Language?)new Language
						{
							Color = lang.Color,
							Name = lang.Name,
						})
						.ToList(),
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}

		public async Task<CustomRepositoryResponseForCodePage> GetCustomDetailsAsync(string owner, string name, CancellationToken cancellationToken = default)
		{
			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.IssueState>> issueState =
				new(new OctokitGraphQLModel.IssueState[] {
					OctokitGraphQLModel.IssueState.Open
				});
			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.PullRequestState>> pullRequestState =
				new(new OctokitGraphQLModel.PullRequestState[] {
					OctokitGraphQLModel.PullRequestState.Open
				});

			var query = new Query()
				.Select(root => new
				{
					first = root.Repository(name, owner, null).Select(x => new Repository
					{
						HomepageUrl = x.HomepageUrl,
						ForkingAllowed = x.ForkingAllowed,
						HasIssuesEnabled = x.HasIssuesEnabled,
						HasProjectsEnabled = x.HasProjectsEnabled,
						IsArchived = x.IsArchived,
						IsEmpty = x.IsEmpty,
						IsPrivate = x.IsPrivate,
						IsTemplate = x.IsTemplate,
						ViewerSubscription = (SubscriptionState?)x.ViewerSubscription,
						Name = x.Name,
						Description = x.Description,
						StargazerCount = x.StargazerCount,
						ForkCount = x.ForkCount,
						IsFork = x.IsFork,
						IsInOrganization = x.IsInOrganization,
						ViewerHasStarred = x.ViewerHasStarred,
						UpdatedAt = x.UpdatedAt,

						LicenseInfo = x.LicenseInfo.Select(licenseInfo => new License
						{
							Name = licenseInfo.Name,
						})
						.SingleOrDefault(),

						DefaultBranchRef = x.DefaultBranchRef.Select(defaultbranchref => new Ref
						{
							Name = defaultbranchref.Name,
						})
						.SingleOrDefault(),

						Watchers = x.Watchers(null, null, null, null).Select(watchers => new UserConnection
						{
							TotalCount = watchers.TotalCount,
						})
						.Single(),

						Releases = x.Releases(null, null, null, null, null).Select(releases => new ReleaseConnection
						{
							TotalCount = releases.TotalCount,
						})
						.Single(),

						Issues = x.Issues(null, null, null, null, null, null, null, issueState).Select(issues => new IssueConnection
						{
							TotalCount = issues.TotalCount
						})
						.Single(),

						PullRequests = x.PullRequests(null, null, null, null, null, null, null, null, pullRequestState).Select(issues => new PullRequestConnection
						{
							TotalCount = issues.TotalCount
						})
						.Single(),

						Owner = x.Owner.Select(owner => new RepositoryOwner
						{
							AvatarUrl = owner.AvatarUrl(500),
							Id = owner.Id,
							Login = owner.Login,
						})
						.Single(),

						LatestRelease = x.LatestRelease.Select(release => new Release
						{
							Description = release.Description,
							DescriptionHTML = release.DescriptionHTML,
							IsDraft = release.IsDraft,
							IsLatest = release.IsLatest,
							IsPrerelease = release.IsPrerelease,
							Name = release.Name,
							PublishedAt = release.PublishedAt,
							PublishedAtHumanized = release.PublishedAt.ToRelativeTime(),
						}).Single(),

						Languages = x.Languages(10, null, null, null, null).Select(langConection => new LanguageConnection
						{
							Nodes = langConection.Nodes.Select(lang => (Language?)new Language
							{
								Color = lang.Color,
								Name = lang.Name,
							}).ToList(),
						}).SingleOrDefault(),
					}).SingleOrDefault(),

					second = root.Repository(name, owner, null).Select(y => new
					{
						Heads = y.Refs("refs/heads/", null, null, null, null, null, null, null).Select(ref1 => new RefConnection
						{
							TotalCount = ref1.TotalCount,
						})
						.SingleOrDefault(),

						Tags = y.Refs("refs/tags/", null, null, null, null, null, null, null).Select(ref2 => new RefConnection
						{
							TotalCount = ref2.TotalCount,
						})
						.SingleOrDefault(),
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return new CustomRepositoryResponseForCodePage()
			{
				Repository = response.first,
				BranchesTotalCount = response.second.Heads.TotalCount,
				TagsTotalCount = response.second.Tags.TotalCount,
			};
		}

		public async Task<(int, int)> GetBranchAndTagCountAsync(string owner, string name, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.Repository(owner: owner, name: name)
				.Select(x => new
				{
					HeadRefsCount = x.Refs("refs/heads/", null, null, null, null, null, null, null).TotalCount,
					TagCount = x.Refs("refs/tags/", null, null, null, null, null, null, null).TotalCount,
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return (response.HeadRefsCount, response.TagCount);
		}

		public async Task<Repository> GetIssueOptionsAsync(
			string owner,
			string name,
			CancellationToken cancellationToken = default)
		{
			var openMilestones = new OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.MilestoneState>>(
				new[] { OctokitGraphQLModel.MilestoneState.Open });

			var query = new Query()
				.Repository(name, owner)
				.Select(repository => new Repository
				{
					AssignableUsers = repository.AssignableUsers(100, null, null, null, null).Select(users => new UserConnection
					{
						Nodes = users.Nodes.Select(user => (User?)new User
						{
							AvatarUrl = user.AvatarUrl(500),
							Id = user.Id,
							Login = user.Login,
							Name = user.Name,
						}).ToList(),
					}).SingleOrDefault(),

					Labels = repository.Labels(100, null, null, null, null, null).Select(labels => new LabelConnection
					{
						Nodes = labels.Nodes.Select(label => (Label?)new Label
						{
							Color = label.Color,
							Description = label.Description,
							Id = label.Id,
							Name = label.Name,
						}).ToList(),
					}).SingleOrDefault(),

					Milestones = repository.Milestones(100, null, null, null, null, null, openMilestones).Select(milestones => new MilestoneConnection
					{
						Nodes = milestones.Nodes.Select(milestone => (Milestone?)new Milestone
						{
							Id = milestone.Id,
							ProgressPercentage = milestone.ProgressPercentage,
							Title = milestone.Title,
						}).ToList(),
					}).SingleOrDefault(),
				})
				.Compile();

			return await _gitHub.RunGraphQLAsync(query, cancellationToken);
		}

		public async Task<(IReadOnlyList<string> Branches, IReadOnlyList<string> Tags)> GetBranchAndTagNamesAsync(
			string owner,
			string name,
			CancellationToken cancellationToken = default)
		{
			ValidateRepository(owner, name);
			var options = new OctokitV3.ApiOptions
			{
				PageCount = int.MaxValue,
				PageSize = 100,
				StartPage = 1,
			};

			return await _gitHub.RunRestAsync(async client =>
			{
				var branchesTask = client.Repository.Branch.GetAll(owner, name, options);
				var tagsTask = client.Repository.GetAllTags(owner, name, options);
				await Task.WhenAll(branchesTask, tagsTask);

				return (
					Branches: (IReadOnlyList<string>)branchesTask.Result
						.Select(branch => branch.Name)
						.Where(branch => !string.IsNullOrWhiteSpace(branch))
						.Distinct(StringComparer.Ordinal)
						.ToList(),
					Tags: (IReadOnlyList<string>)tagsTask.Result
						.Select(tag => tag.Name)
						.Where(tag => !string.IsNullOrWhiteSpace(tag))
						.Distinct(StringComparer.Ordinal)
						.ToList());
			}, cancellationToken);
		}

		public Task<string> GetReadmeMarkdownAsync(string owner, string name, CancellationToken cancellationToken = default)
		{
			ValidateRepository(owner, name);
			if (_cache is null)
				return GetReadmeMarkdownUncachedAsync(owner, name, cancellationToken);

			return _cache.GetOrCreateAsync(
				CreateRepositoryKey("repository-readme", owner, name),
				CachePolicies.Repository,
				CacheSerializers.String,
				token => GetReadmeMarkdownUncachedAsync(owner, name, token),
				cancellationToken);
		}

		private async Task<string> GetReadmeMarkdownUncachedAsync(string owner, string name, CancellationToken cancellationToken)
		{
			try
			{
				var readme = await _gitHub.RunRestAsync(
					client => client.Repository.Content.GetReadme(owner, name),
					cancellationToken);
				return readme.Content;
			}
			catch (global::Octokit.NotFoundException)
			{
				return string.Empty;
			}
		}

		private CacheKey CreateRepositoryKey(string category, string owner, string name)
			=> CacheKey.ForAccount(
				_gitHub.CachePartition,
				category,
				$"{owner.Trim().ToLowerInvariant()}/{name.Trim().ToLowerInvariant()}");

		private static void ValidateRepository(string owner, string name)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(owner);
			ArgumentException.ThrowIfNullOrWhiteSpace(name);
		}
	}
}
