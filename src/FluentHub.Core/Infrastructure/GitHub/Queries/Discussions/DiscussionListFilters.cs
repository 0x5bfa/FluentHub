namespace FluentHub.Core.Infrastructure.GitHub.Queries.Discussions
{
	public enum DiscussionStateFilter
	{
		Open,
		Closed,
		Locked,
		Unlocked,
		Answered,
		Unanswered,
		Verified,
		All,
	}

	public enum DiscussionSort
	{
		LatestActivity,
		DateCreated,
		TopPastDay,
		TopPastWeek,
		TopPastMonth,
		TopPastYear,
		TopAllTime,
	}

	public sealed record DiscussionListFilters
	{
		public DiscussionStateFilter State { get; init; } = DiscussionStateFilter.Open;

		public DiscussionSort Sort { get; init; } = DiscussionSort.LatestActivity;

		public string? SearchText { get; init; }

		public string? Label { get; init; }
	}

	internal static class DiscussionSearchQueryBuilder
	{
		public static string BuildForRepository(
			string owner,
			string name,
			DiscussionListFilters filters,
			DateOnly? currentDate = null)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(owner);
			ArgumentException.ThrowIfNullOrWhiteSpace(name);

			return Build(
				$"repo:{owner.Trim()}/{name.Trim()}",
				filters,
				currentDate);
		}

		public static string BuildForAuthor(
			string login,
			DiscussionListFilters filters,
			DateOnly? currentDate = null)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);

			return Build(
				$"author:{Quote(login.Trim())}",
				filters,
				currentDate);
		}

		private static string Build(
			string scope,
			DiscussionListFilters filters,
			DateOnly? currentDate)
		{
			ArgumentNullException.ThrowIfNull(filters);

			var terms = new List<string> { scope };
			if (!string.IsNullOrWhiteSpace(filters.SearchText))
				terms.Add(filters.SearchText.Trim());

			terms.AddRange(filters.State switch
			{
				DiscussionStateFilter.Open => ["is:open"],
				DiscussionStateFilter.Closed => ["is:closed"],
				DiscussionStateFilter.Locked => ["is:open", "is:locked"],
				DiscussionStateFilter.Unlocked => ["is:open", "is:unlocked"],
				DiscussionStateFilter.Answered => ["is:open", "is:answered"],
				DiscussionStateFilter.Unanswered => ["is:open", "is:unanswered"],
				DiscussionStateFilter.Verified => ["is:open", "is:verified"],
				DiscussionStateFilter.All => [],
				_ => throw new ArgumentOutOfRangeException(nameof(filters.State), filters.State, "Unsupported discussion state filter."),
			});

			if (!string.IsNullOrWhiteSpace(filters.Label))
				terms.Add($"label:{Quote(filters.Label.Trim())}");

			var today = currentDate ?? DateOnly.FromDateTime(DateTime.UtcNow);
			var sortTerms = filters.Sort switch
			{
				DiscussionSort.LatestActivity => [],
				DiscussionSort.DateCreated => ["sort:date_created"],
				DiscussionSort.TopPastDay => TopSince(today.AddDays(-1)),
				DiscussionSort.TopPastWeek => TopSince(today.AddDays(-7)),
				DiscussionSort.TopPastMonth => TopSince(today.AddMonths(-1)),
				DiscussionSort.TopPastYear => TopSince(today.AddYears(-1)),
				DiscussionSort.TopAllTime => ["sort:top"],
				_ => throw new ArgumentOutOfRangeException(nameof(filters.Sort), filters.Sort, "Unsupported discussion sort."),
			};
			terms.AddRange(sortTerms);

			return string.Join(' ', terms);
		}

		private static string[] TopSince(DateOnly date)
			=> ["sort:top", $"created:>={date:yyyy-MM-dd}"];

		private static string Quote(string value)
			=> $"\"{value.Replace("\\", "\\\\", StringComparison.Ordinal).Replace("\"", "\\\"", StringComparison.Ordinal)}\"";
	}
}
