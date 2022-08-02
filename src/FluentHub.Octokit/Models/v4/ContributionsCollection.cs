namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A contributions collection aggregates contributions such as opened issues and commits created by a user.
    /// </summary>
    public class ContributionsCollection
    {
        /// <summary>
        /// Commit contributions made by the user, grouped by repository.
        /// </summary>
        /// <param name="maxRepositories">How many repositories should be included.</param>
        public List<CommitContributionsByRepository> CommitContributionsByRepository { get; set; }

        /// <summary>
        /// A calendar of this user's contributions on GitHub.
        /// </summary>
        public ContributionCalendar ContributionCalendar { get; set; }

        /// <summary>
        /// The years the user has been making contributions with the most recent year first.
        /// </summary>
        public List<int> ContributionYears { get; set; }

        /// <summary>
        /// Determine if this collection's time span ends in the current month.
        /// </summary>
        public bool DoesEndInCurrentMonth { get; set; }

        /// <summary>
        /// The date of the first restricted contribution the user made in this time period. Can only be non-null when the user has enabled private contribution counts.
        /// </summary>
        public string EarliestRestrictedContributionDate { get; set; }

        /// <summary>
        /// The ending date and time of this collection.
        /// </summary>
        public DateTimeOffset EndedAt { get; set; }

        /// <summary>
        /// The first issue the user opened on GitHub. This will be null if that issue was opened outside the collection's time range and ignoreTimeRange is false. If the issue is not visible but the user has opted to show private contributions, a RestrictedContribution will be returned.
        /// </summary>
        public CreatedIssueOrRestrictedContribution FirstIssueContribution { get; set; }

        /// <summary>
        /// The first pull request the user opened on GitHub. This will be null if that pull request was opened outside the collection's time range and ignoreTimeRange is not true. If the pull request is not visible but the user has opted to show private contributions, a RestrictedContribution will be returned.
        /// </summary>
        public CreatedPullRequestOrRestrictedContribution FirstPullRequestContribution { get; set; }

        /// <summary>
        /// The first repository the user created on GitHub. This will be null if that first repository was created outside the collection's time range and ignoreTimeRange is false. If the repository is not visible, then a RestrictedContribution is returned.
        /// </summary>
        public CreatedRepositoryOrRestrictedContribution FirstRepositoryContribution { get; set; }

        /// <summary>
        /// Does the user have any more activity in the timeline that occurred prior to the collection's time range?
        /// </summary>
        public bool HasActivityInThePast { get; set; }

        /// <summary>
        /// Determine if there are any contributions in this collection.
        /// </summary>
        public bool HasAnyContributions { get; set; }

        /// <summary>
        /// Determine if the user made any contributions in this time frame whose details are not visible because they were made in a private repository. Can only be true if the user enabled private contribution counts.
        /// </summary>
        public bool HasAnyRestrictedContributions { get; set; }

        /// <summary>
        /// Whether or not the collector's time span is all within the same day.
        /// </summary>
        public bool IsSingleDay { get; set; }

        /// <summary>
        /// A list of issues the user opened.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="excludeFirst">Should the user's first issue ever be excluded from the result.</param>
        /// <param name="excludePopular">Should the user's most commented issue be excluded from the result.</param>
        /// <param name="orderBy">Ordering options for contributions returned from the connection.</param>
        public CreatedIssueContributionConnection IssueContributions { get; set; }

        /// <summary>
        /// Issue contributions made by the user, grouped by repository.
        /// </summary>
        /// <param name="excludeFirst">Should the user's first issue ever be excluded from the result.</param>
        /// <param name="excludePopular">Should the user's most commented issue be excluded from the result.</param>
        /// <param name="maxRepositories">How many repositories should be included.</param>
        public List<IssueContributionsByRepository> IssueContributionsByRepository { get; set; }

        /// <summary>
        /// When the user signed up for GitHub. This will be null if that sign up date falls outside the collection's time range and ignoreTimeRange is false.
        /// </summary>
        public JoinedGitHubContribution JoinedGitHubContribution { get; set; }

        /// <summary>
        /// The date of the most recent restricted contribution the user made in this time period. Can only be non-null when the user has enabled private contribution counts.
        /// </summary>
        public string LatestRestrictedContributionDate { get; set; }

        /// <summary>
        /// When this collection's time range does not include any activity from the user, use this
        /// to get a different collection from an earlier time range that does have activity.
        /// </summary>
        public ContributionsCollection MostRecentCollectionWithActivity { get; set; }

        /// <summary>
        /// Returns a different contributions collection from an earlier time range than this one
        /// that does not have any contributions.
        /// </summary>
        public ContributionsCollection MostRecentCollectionWithoutActivity { get; set; }

        /// <summary>
        /// The issue the user opened on GitHub that received the most comments in the specified
        /// time frame.
        /// </summary>
        public CreatedIssueContribution PopularIssueContribution { get; set; }

        /// <summary>
        /// The pull request the user opened on GitHub that received the most comments in the
        /// specified time frame.
        /// </summary>
        public CreatedPullRequestContribution PopularPullRequestContribution { get; set; }

        /// <summary>
        /// Pull request contributions made by the user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="excludeFirst">Should the user's first pull request ever be excluded from the result.</param>
        /// <param name="excludePopular">Should the user's most commented pull request be excluded from the result.</param>
        /// <param name="orderBy">Ordering options for contributions returned from the connection.</param>
        public CreatedPullRequestContributionConnection PullRequestContributions { get; set; }

        /// <summary>
        /// Pull request contributions made by the user, grouped by repository.
        /// </summary>
        /// <param name="excludeFirst">Should the user's first pull request ever be excluded from the result.</param>
        /// <param name="excludePopular">Should the user's most commented pull request be excluded from the result.</param>
        /// <param name="maxRepositories">How many repositories should be included.</param>
        public List<PullRequestContributionsByRepository> PullRequestContributionsByRepository { get; set; }

        /// <summary>
        /// Pull request review contributions made by the user. Returns the most recently
        /// submitted review for each PR reviewed by the user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for contributions returned from the connection.</param>
        public CreatedPullRequestReviewContributionConnection PullRequestReviewContributions { get; set; }

        /// <summary>
        /// Pull request review contributions made by the user, grouped by repository.
        /// </summary>
        /// <param name="maxRepositories">How many repositories should be included.</param>
        public List<PullRequestReviewContributionsByRepository> PullRequestReviewContributionsByRepository { get; set; }

        /// <summary>
        /// A list of repositories owned by the user that the user created in this time range.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="excludeFirst">Should the user's first repository ever be excluded from the result.</param>
        /// <param name="orderBy">Ordering options for contributions returned from the connection.</param>
        public CreatedRepositoryContributionConnection RepositoryContributions { get; set; }

        /// <summary>
        /// A count of contributions made by the user that the viewer cannot access. Only non-zero when the user has chosen to share their private contribution counts.
        /// </summary>
        public int RestrictedContributionsCount { get; set; }

        /// <summary>
        /// The beginning date and time of this collection.
        /// </summary>
        public DateTimeOffset StartedAt { get; set; }

        /// <summary>
        /// How many commits were made by the user in this time span.
        /// </summary>
        public int TotalCommitContributions { get; set; }

        /// <summary>
        /// How many issues the user opened.
        /// </summary>
        /// <param name="excludeFirst">Should the user's first issue ever be excluded from this count.</param>
        /// <param name="excludePopular">Should the user's most commented issue be excluded from this count.</param>
        public int TotalIssueContributions { get; set; }

        /// <summary>
        /// How many pull requests the user opened.
        /// </summary>
        /// <param name="excludeFirst">Should the user's first pull request ever be excluded from this count.</param>
        /// <param name="excludePopular">Should the user's most commented pull request be excluded from this count.</param>
        public int TotalPullRequestContributions { get; set; }

        /// <summary>
        /// How many pull request reviews the user left.
        /// </summary>
        public int TotalPullRequestReviewContributions { get; set; }

        /// <summary>
        /// How many different repositories the user committed to.
        /// </summary>
        public int TotalRepositoriesWithContributedCommits { get; set; }

        /// <summary>
        /// How many different repositories the user opened issues in.
        /// </summary>
        /// <param name="excludeFirst">Should the user's first issue ever be excluded from this count.</param>
        /// <param name="excludePopular">Should the user's most commented issue be excluded from this count.</param>
        public int TotalRepositoriesWithContributedIssues { get; set; }

        /// <summary>
        /// How many different repositories the user left pull request reviews in.
        /// </summary>
        public int TotalRepositoriesWithContributedPullRequestReviews { get; set; }

        /// <summary>
        /// How many different repositories the user opened pull requests in.
        /// </summary>
        /// <param name="excludeFirst">Should the user's first pull request ever be excluded from this count.</param>
        /// <param name="excludePopular">Should the user's most commented pull request be excluded from this count.</param>
        public int TotalRepositoriesWithContributedPullRequests { get; set; }

        /// <summary>
        /// How many repositories the user created.
        /// </summary>
        /// <param name="excludeFirst">Should the user's first repository ever be excluded from this count.</param>
        public int TotalRepositoryContributions { get; set; }

        /// <summary>
        /// The user who made the contributions in this collection.
        /// </summary>
        public User User { get; set; }
    }
}