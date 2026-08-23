using FluentHub.Core.Queries.Discussions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class DiscussionListFiltersTests
{
	[TestMethod]
	public void RepositoryDiscussionQueryUsesGitHubFilterQualifiers()
	{
		var query = DiscussionSearchQueryBuilder.BuildForRepository(
			"octocat",
			"hello-world",
			new DiscussionListFilters
			{
				SearchText = "release notes",
				State = DiscussionStateFilter.Locked,
				Label = "Product feedback",
				Sort = DiscussionSort.DateCreated,
			});

		Assert.AreEqual(
			"repo:octocat/hello-world release notes is:open is:locked label:\"Product feedback\" sort:date_created",
			query);
	}

	[TestMethod]
	public void TopDiscussionSortUsesTheSelectedTimeWindow()
	{
		var query = DiscussionSearchQueryBuilder.BuildForAuthor(
			"octocat",
			new DiscussionListFilters
			{
				State = DiscussionStateFilter.All,
				Sort = DiscussionSort.TopPastWeek,
			},
			new DateOnly(2026, 8, 24));

		Assert.AreEqual(
			"author:\"octocat\" sort:top created:>=2026-08-17",
			query);
	}
}
