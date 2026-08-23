using FluentHub.Core.Contracts;
using FluentHub.Core.Queries.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class UserRepositoryListFiltersTests
{
	[TestMethod]
	public void SearchQueryIncludesUserTextTypeLanguageAndSort()
	{
		var query = UserRepositorySearchQueryBuilder.Build(
			"octocat",
			new UserRepositoryListFilters
			{
				SearchText = "hello world",
				Type = UserRepositoryTypeFilter.Private,
				Language = "C#",
				Sort = UserRepositorySort.Stars,
			});

		Assert.AreEqual(
			"\"hello world\" in:name user:\"octocat\" is:private fork:true language:\"C#\" sort:stars-desc",
			query);
	}

	[TestMethod]
	public void SearchQueryUsesGitHubRepositoryTypeQualifiers()
	{
		Assert.IsTrue(Build(UserRepositoryTypeFilter.All).Contains("fork:true", StringComparison.Ordinal));
		Assert.IsFalse(Build(UserRepositoryTypeFilter.Sources).Contains("fork:", StringComparison.Ordinal));
		Assert.IsTrue(Build(UserRepositoryTypeFilter.Forks).Contains("fork:only", StringComparison.Ordinal));
		Assert.IsTrue(Build(UserRepositoryTypeFilter.Archived).Contains("archived:true", StringComparison.Ordinal));
		Assert.IsTrue(Build(UserRepositoryTypeFilter.Sponsorable).Contains("is:sponsorable", StringComparison.Ordinal));
		Assert.IsTrue(Build(UserRepositoryTypeFilter.Mirrors).Contains("mirror:true", StringComparison.Ordinal));
		Assert.IsTrue(Build(UserRepositoryTypeFilter.Templates).Contains("template:true", StringComparison.Ordinal));
	}

	[TestMethod]
	public void StarredRepositoryFiltersUseRepositoryMetadata()
	{
		var repositories = new[]
		{
			CreateRepository("PublicSource", "C#", stars: 2),
			CreateRepository("PrivateFork", "TypeScript", stars: 8, isPrivate: true, isFork: true),
			CreateRepository("Template", "C#", stars: 5, isTemplate: true),
		};

		var result = UserRepositoryFilter.Apply(
			repositories,
			new StarredRepositoryListFilters
			{
				SearchText = "temp",
				Language = "c#",
				Type = UserRepositoryTypeFilter.Templates,
				Sort = StarredRepositorySort.MostStars,
			});

		Assert.HasCount(1, result);
		Assert.AreEqual("Template", result[0].Name);
	}

	[TestMethod]
	public void StarredRepositoriesCanSortByActivityAndStars()
	{
		var olderPopular = CreateRepository("Popular", "C#", stars: 10);
		olderPopular.PushedAt = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
		var recent = CreateRepository("Recent", "C#", stars: 1);
		recent.PushedAt = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);

		var byActivity = UserRepositoryFilter.Apply(
			[olderPopular, recent],
			new StarredRepositoryListFilters { Sort = StarredRepositorySort.RecentlyActive });
		var byStars = UserRepositoryFilter.Apply(
			[olderPopular, recent],
			new StarredRepositoryListFilters { Sort = StarredRepositorySort.MostStars });

		Assert.AreEqual("Recent", byActivity[0].Name);
		Assert.AreEqual("Popular", byStars[0].Name);
	}

	private static string Build(UserRepositoryTypeFilter type)
		=> UserRepositorySearchQueryBuilder.Build(
			"octocat",
			new UserRepositoryListFilters { Type = type });

	private static Repository CreateRepository(
		string name,
		string language,
		int stars,
		bool isPrivate = false,
		bool isFork = false,
		bool isTemplate = false)
		=> new()
		{
			Name = name,
			PrimaryLanguage = new Language { Name = language },
			StargazerCount = stars,
			IsPrivate = isPrivate,
			IsFork = isFork,
			IsTemplate = isTemplate,
		};
}
