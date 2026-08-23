using FluentHub.Core.Queries.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class RepositoryItemSearchQueryBuilderTests
{
	[TestMethod]
	public void DefaultQueryRequestsOnlyOpenIssuesByNewestFirst()
	{
		var query = RepositoryItemSearchQueryBuilder.Build(
			"owner",
			"repository",
			false,
			new RepositoryItemListFilters());

		Assert.AreEqual("repo:owner/repository is:issue is:open sort:created-desc", query);
		Assert.IsFalse(query.Contains("is:closed", StringComparison.Ordinal));
	}

	[TestMethod]
	public void QueryIncludesSelectedFiltersAndNoValueQualifiers()
	{
		var query = RepositoryItemSearchQueryBuilder.Build(
			"owner",
			"repository",
			false,
			new RepositoryItemListFilters
			{
				State = RepositoryItemStateFilter.All,
				Sort = RepositoryItemSort.BestMatch,
				SearchText = "crash now",
				Label = "help wanted",
				IssueType = "Bug",
				Author = "octocat",
				HasNoAssignee = true,
				HasNoMilestone = true,
			});

		Assert.AreEqual(
			"repo:owner/repository is:issue \"crash now\" label:\"help wanted\" type:\"Bug\" author:\"octocat\" no:assignee no:milestone",
			query);
		Assert.IsFalse(query.Contains("is:open", StringComparison.Ordinal));
		Assert.IsFalse(query.Contains("is:closed", StringComparison.Ordinal));
		Assert.IsFalse(query.Contains("sort:", StringComparison.Ordinal));
	}

	[TestMethod]
	public void PullRequestQueryIgnoresIssueTypeFilters()
	{
		var query = RepositoryItemSearchQueryBuilder.Build(
			"owner",
			"repository",
			true,
			new RepositoryItemListFilters
			{
				IssueType = "Bug",
				HasNoIssueType = true,
			});

		Assert.IsTrue(query.Contains("is:pr", StringComparison.Ordinal));
		Assert.IsFalse(query.Contains("type:", StringComparison.Ordinal));
		Assert.IsFalse(query.Contains("no:type", StringComparison.Ordinal));
	}

	[TestMethod]
	[DataRow(RepositoryItemSort.MostThumbsUp, "sort:reactions-+1-desc")]
	[DataRow(RepositoryItemSort.MostThumbsDown, "sort:reactions--1-desc")]
	[DataRow(RepositoryItemSort.MostLaugh, "sort:reactions-smile-desc")]
	[DataRow(RepositoryItemSort.MostHooray, "sort:reactions-tada-desc")]
	[DataRow(RepositoryItemSort.MostConfused, "sort:reactions-confused-desc")]
	[DataRow(RepositoryItemSort.MostHeart, "sort:reactions-heart-desc")]
	[DataRow(RepositoryItemSort.MostRocket, "sort:reactions-rocket-desc")]
	[DataRow(RepositoryItemSort.MostEyes, "sort:reactions-eyes-desc")]
	public void ReactionSortUsesGitHubSearchQualifier(RepositoryItemSort sort, string expected)
	{
		var query = RepositoryItemSearchQueryBuilder.Build(
			"owner",
			"repository",
			false,
			new RepositoryItemListFilters { Sort = sort });

		Assert.IsTrue(query.EndsWith(expected, StringComparison.Ordinal));
	}
}
