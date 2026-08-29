namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public enum RepositoryItemStateFilter
	{
		Open,
		Closed,
		All,
	}

	public enum RepositoryItemSort
	{
		Newest,
		Oldest,
		MostCommented,
		LeastCommented,
		RecentlyUpdated,
		LeastRecentlyUpdated,
		BestMatch,
		MostThumbsUp,
		MostThumbsDown,
		MostLaugh,
		MostHooray,
		MostConfused,
		MostHeart,
		MostRocket,
		MostEyes,
	}

	public sealed record RepositoryItemListFilters
	{
		public RepositoryItemStateFilter State { get; init; } = RepositoryItemStateFilter.Open;

		public RepositoryItemSort Sort { get; init; } = RepositoryItemSort.Newest;

		public string? SearchText { get; init; }

		public string? Label { get; init; }

		public bool HasNoLabels { get; init; }

		public string? IssueType { get; init; }

		public bool HasNoIssueType { get; init; }

		public string? Author { get; init; }

		public string? Assignee { get; init; }

		public bool HasNoAssignee { get; init; }

		public string? Milestone { get; init; }

		public bool HasNoMilestone { get; init; }
	}

	public sealed record RepositoryItemFilterOptions
	{
		public IReadOnlyList<string> Labels { get; init; } = [];

		public IReadOnlyList<string> IssueTypes { get; init; } = [];

		public IReadOnlyList<string> Assignees { get; init; } = [];

		public IReadOnlyList<string> Milestones { get; init; } = [];
	}

	internal static class RepositoryItemSearchQueryBuilder
	{
		public static string Build(
			string owner,
			string name,
			bool isPullRequest,
			RepositoryItemListFilters filters)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(owner);
			ArgumentException.ThrowIfNullOrWhiteSpace(name);
			return Build(
				[$"repo:{owner.Trim()}/{name.Trim()}", isPullRequest ? "is:pr" : "is:issue"],
				isPullRequest,
				filters);
		}

		public static string BuildForAuthor(
			string login,
			bool isPullRequest,
			RepositoryItemListFilters filters)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);

			return Build(
				[$"author:{Quote(login.Trim())}", isPullRequest ? "is:pr" : "is:issue"],
				isPullRequest,
				filters);
		}

		private static string Build(
			List<string> terms,
			bool isPullRequest,
			RepositoryItemListFilters filters)
		{
			ArgumentNullException.ThrowIfNull(filters);

			if (!string.IsNullOrWhiteSpace(filters.SearchText))
				terms.Add(Quote(filters.SearchText.Trim()));

			switch (filters.State)
			{
				case RepositoryItemStateFilter.Open:
					terms.Add("is:open");
					break;
				case RepositoryItemStateFilter.Closed:
					terms.Add("is:closed");
					break;
			}

			AppendOptionalQualifier(terms, "label", filters.Label, filters.HasNoLabels);

			if (!isPullRequest)
				AppendOptionalQualifier(terms, "type", filters.IssueType, filters.HasNoIssueType);

			AppendOptionalQualifier(terms, "author", filters.Author);
			AppendOptionalQualifier(terms, "assignee", filters.Assignee, filters.HasNoAssignee);
			AppendOptionalQualifier(terms, "milestone", filters.Milestone, filters.HasNoMilestone);

			var sort = filters.Sort switch
			{
				RepositoryItemSort.Newest => "sort:created-desc",
				RepositoryItemSort.Oldest => "sort:created-asc",
				RepositoryItemSort.MostCommented => "sort:comments-desc",
				RepositoryItemSort.LeastCommented => "sort:comments-asc",
				RepositoryItemSort.RecentlyUpdated => "sort:updated-desc",
				RepositoryItemSort.LeastRecentlyUpdated => "sort:updated-asc",
				RepositoryItemSort.BestMatch => null,
				RepositoryItemSort.MostThumbsUp => "sort:reactions-+1-desc",
				RepositoryItemSort.MostThumbsDown => "sort:reactions--1-desc",
				RepositoryItemSort.MostLaugh => "sort:reactions-smile-desc",
				RepositoryItemSort.MostHooray => "sort:reactions-tada-desc",
				RepositoryItemSort.MostConfused => "sort:reactions-confused-desc",
				RepositoryItemSort.MostHeart => "sort:reactions-heart-desc",
				RepositoryItemSort.MostRocket => "sort:reactions-rocket-desc",
				RepositoryItemSort.MostEyes => "sort:reactions-eyes-desc",
				_ => throw new ArgumentOutOfRangeException(nameof(filters.Sort), filters.Sort, "Unsupported sort option."),
			};

			if (sort is not null)
				terms.Add(sort);

			return string.Join(' ', terms);
		}

		private static void AppendOptionalQualifier(
			List<string> terms,
			string qualifier,
			string? value,
			bool hasNoValue = false)
		{
			if (hasNoValue)
			{
				terms.Add($"no:{qualifier}");
				return;
			}

			if (!string.IsNullOrWhiteSpace(value))
				terms.Add($"{qualifier}:{Quote(value.Trim())}");
		}

		private static string Quote(string value)
			=> $"\"{value.Replace("\\", "\\\\", StringComparison.Ordinal).Replace("\"", "\\\"", StringComparison.Ordinal)}\"";
	}
}
