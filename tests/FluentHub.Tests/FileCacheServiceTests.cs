using FluentHub.Core.Application.Abstractions.Caching;
using FluentHub.Core.Infrastructure.Caching;
using FluentHub.Core.Application.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit.GraphQL;

namespace FluentHub.Tests;

[TestClass]
public sealed class FileCacheServiceTests
{
	private string _cachePath = null!;

	[TestInitialize]
	public void Initialize()
		=> _cachePath = Path.Combine(Path.GetTempPath(), "FluentHub.Tests", Guid.NewGuid().ToString("N"));

	[TestCleanup]
	public void Cleanup()
	{
		if (Directory.Exists(_cachePath))
			Directory.Delete(_cachePath, recursive: true);
	}

	[TestMethod]
	public async Task GetOrCreateAsyncUsesMemoryAndDiskCache()
	{
		var key = CacheKey.Shared("test", "persistent");
		var policy = new CachePolicy(TimeSpan.FromHours(1), TimeSpan.FromDays(1));
		var factoryCalls = 0;
		var cache = new FileCacheService(_cachePath);

		var first = await cache.GetOrCreateAsync(
			key,
			policy,
			CacheSerializers.String,
			_ => Task.FromResult((++factoryCalls).ToString()));
		var second = await cache.GetOrCreateAsync(
			key,
			policy,
			CacheSerializers.String,
			_ => Task.FromResult((++factoryCalls).ToString()));
		var reloadedCache = new FileCacheService(_cachePath);
		var third = await reloadedCache.GetOrCreateAsync(
			key,
			policy,
			CacheSerializers.String,
			_ => Task.FromResult((++factoryCalls).ToString()));

		Assert.AreEqual("1", first);
		Assert.AreEqual("1", second);
		Assert.AreEqual("1", third);
		Assert.AreEqual(1, factoryCalls);
	}

	[TestMethod]
	public async Task GetOrCreateAsyncCoalescesConcurrentRequests()
	{
		var cache = new FileCacheService(_cachePath);
		var key = CacheKey.Shared("test", "single-flight");
		var policy = new CachePolicy(TimeSpan.FromHours(1), TimeSpan.FromDays(1));
		var releaseFactory = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
		var factoryCalls = 0;

		async Task<string> Factory(CancellationToken cancellationToken)
		{
			Interlocked.Increment(ref factoryCalls);
			await releaseFactory.Task.WaitAsync(cancellationToken);
			return "cached";
		}

		var requests = Enumerable.Range(0, 8)
			.Select(_ => cache.GetOrCreateAsync(key, policy, CacheSerializers.String, Factory))
			.ToArray();
		releaseFactory.SetResult();

		var results = await Task.WhenAll(requests);

		CollectionAssert.AreEqual(Enumerable.Repeat("cached", 8).ToArray(), results);
		Assert.AreEqual(1, factoryCalls);
	}

	[TestMethod]
	public async Task StaleEntryIsReturnedWhileItRefreshes()
	{
		var timeProvider = new ManualTimeProvider(new DateTimeOffset(2026, 8, 23, 0, 0, 0, TimeSpan.Zero));
		var cache = new FileCacheService(_cachePath, timeProvider: timeProvider);
		var key = CacheKey.Shared("test", "stale");
		var policy = new CachePolicy(TimeSpan.FromMinutes(1), TimeSpan.FromDays(1));
		var refreshed = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
		var factoryCalls = 0;

		Task<string> Factory(CancellationToken _)
		{
			var call = Interlocked.Increment(ref factoryCalls);
			if (call == 2)
				refreshed.SetResult();
			return Task.FromResult(call == 1 ? "old" : "new");
		}

		Assert.AreEqual("old", await cache.GetOrCreateAsync(key, policy, CacheSerializers.String, Factory));
		timeProvider.Advance(TimeSpan.FromMinutes(2));

		Assert.AreEqual("old", await cache.GetOrCreateAsync(key, policy, CacheSerializers.String, Factory));
		await refreshed.Task.WaitAsync(TimeSpan.FromSeconds(5));

		for (var attempt = 0; attempt < 20; attempt++)
		{
			if (await cache.GetOrCreateAsync(key, policy, CacheSerializers.String, Factory) == "new")
				break;

			await Task.Delay(25);
		}

		Assert.AreEqual("new", await cache.GetOrCreateAsync(key, policy, CacheSerializers.String, Factory));
		Assert.AreEqual(2, factoryCalls);
	}

	[TestMethod]
	public async Task ClearAsyncRemovesMemoryAndDiskEntries()
	{
		var cache = new FileCacheService(_cachePath);
		var key = CacheKey.Shared("test", "clear");
		var policy = new CachePolicy(TimeSpan.FromHours(1), TimeSpan.FromDays(1));

		await cache.GetOrCreateBytesAsync(key, policy, _ => Task.FromResult(new byte[] { 1, 2, 3 }));
		Assert.IsGreaterThan(0, await cache.GetSizeAsync());

		await cache.ClearAsync();

		Assert.AreEqual(0, await cache.GetSizeAsync());
		var bytes = await cache.GetOrCreateBytesAsync(key, policy, _ => Task.FromResult(new byte[] { 4 }));
		CollectionAssert.AreEqual(new byte[] { 4 }, bytes);
	}

	[TestMethod]
	public async Task GitHubRepositorySerializerRestoresInterfacePropertiesFromDisk()
	{
		var cache = new FileCacheService(_cachePath);
		var key = CacheKey.ForAccount("account-test", "repositories", "owner/name");
		var policy = new CachePolicy(TimeSpan.FromHours(1), TimeSpan.FromDays(1));
		var repository = new Repository
		{
			Id = new ID("repository-id"),
			Name = "FluentHub",
			Description = "A fluent GitHub client",
			Owner = new RepositoryOwner
			{
				Id = new ID("owner-id"),
				AvatarUrl = "https://avatars.githubusercontent.com/u/1",
				Login = "owner",
			},
			Issues = new IssueConnection { TotalCount = 3 },
			LatestRelease = new Release { Description = "# Changes" },
			PullRequests = new PullRequestConnection { TotalCount = 2 },
			PrimaryLanguage = new Language { Name = "C#", Color = "#178600" },
		};

		await cache.GetOrCreateAsync(
			key,
			policy,
			GitHubCacheSerializers.Repository,
			_ => Task.FromResult(repository));

		var reloadedCache = new FileCacheService(_cachePath);
		var restored = await reloadedCache.GetOrCreateAsync(
			key,
			policy,
			GitHubCacheSerializers.Repository,
			_ => throw new AssertFailedException("The disk cache should have been used."));

		Assert.AreEqual("repository-id", restored.Id.Value);
		Assert.IsInstanceOfType<RepositoryOwner>(restored.Owner);
		Assert.AreEqual("owner", restored.Owner.Login);
		Assert.AreEqual(3, restored.Issues.TotalCount);
		Assert.AreEqual("# Changes", restored.LatestRelease?.Description);
		Assert.AreEqual(2, restored.PullRequests.TotalCount);
		Assert.AreEqual("C#", restored.PrimaryLanguage?.Name);
	}

	[TestMethod]
	public void GitHubProfileSerializersPreserveNamesAndDescriptions()
	{
		var user = new User
		{
			AvatarUrl = "https://avatars.githubusercontent.com/u/1",
			Bio = "Developer",
			Email = "dev@example.com",
			Login = "octocat",
			Name = "The Octocat",
		};
		var organization = new Organization
		{
			AvatarUrl = "https://avatars.githubusercontent.com/u/2",
			Description = "Open source organization",
			Id = new ID("organization-id"),
			Login = "github",
			Name = "GitHub",
			Url = "https://github.com/github",
		};

		var restoredUser = GitHubCacheSerializers.User.Deserialize(
			GitHubCacheSerializers.User.Serialize(user));
		var restoredOrganization = GitHubCacheSerializers.Organization.Deserialize(
			GitHubCacheSerializers.Organization.Serialize(organization));

		Assert.AreEqual("octocat", restoredUser.Login);
		Assert.AreEqual("Developer", restoredUser.Bio);
		Assert.AreEqual("github", restoredOrganization.Login);
		Assert.AreEqual("Open source organization", restoredOrganization.Description);
	}

	[TestMethod]
	public void ProfileReadmeSerializerPreservesMarkdownAndDefaultBranch()
	{
		var profileReadme = new ProfileReadme
		{
			DefaultBranchName = "main",
			Markdown = "# Hello",
			OwnerLogin = "octocat",
			RepositoryName = "Octocat",
		};

		var restored = GitHubCacheSerializers.ProfileReadme.Deserialize(
			GitHubCacheSerializers.ProfileReadme.Serialize(profileReadme));

		Assert.AreEqual("main", restored.DefaultBranchName);
		Assert.AreEqual("# Hello", restored.Markdown);
		Assert.AreEqual("octocat", restored.OwnerLogin);
		Assert.AreEqual("Octocat", restored.RepositoryName);
	}

	private sealed class ManualTimeProvider(DateTimeOffset now) : TimeProvider
	{
		private DateTimeOffset _now = now;

		public override DateTimeOffset GetUtcNow()
			=> _now;

		public void Advance(TimeSpan duration)
			=> _now = _now.Add(duration);
	}
}
