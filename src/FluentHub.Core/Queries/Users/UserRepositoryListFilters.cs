namespace FluentHub.Core.Queries.Users
{
	public enum UserRepositoryTypeFilter
	{
		All,
		Public,
		Private,
		Sources,
		Forks,
		Archived,
		Sponsorable,
		Mirrors,
		Templates,
	}

	public enum UserRepositorySort
	{
		Name,
		Stars,
	}

	public enum StarredRepositorySort
	{
		RecentlyStarred,
		RecentlyActive,
		MostStars,
	}

	public sealed record UserRepositoryListFilters
	{
		public string? SearchText { get; init; }

		public UserRepositoryTypeFilter Type { get; init; }

		public string? Language { get; init; }

		public UserRepositorySort Sort { get; init; }
	}

	public sealed record StarredRepositoryListFilters
	{
		public string? SearchText { get; init; }

		public UserRepositoryTypeFilter Type { get; init; }

		public string? Language { get; init; }

		public StarredRepositorySort Sort { get; init; }
	}

	internal static class UserRepositorySearchQueryBuilder
	{
		public static string Build(string login, UserRepositoryListFilters filters)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);
			ArgumentNullException.ThrowIfNull(filters);

			var terms = new List<string>();
			if (!string.IsNullOrWhiteSpace(filters.SearchText))
			{
				terms.Add(Quote(filters.SearchText.Trim()));
				terms.Add("in:name");
			}

			terms.Add($"user:{Quote(login.Trim())}");

			var typeQualifier = filters.Type switch
			{
				UserRepositoryTypeFilter.All => "fork:true",
				UserRepositoryTypeFilter.Public => "is:public fork:true",
				UserRepositoryTypeFilter.Private => "is:private fork:true",
				UserRepositoryTypeFilter.Sources => null,
				UserRepositoryTypeFilter.Forks => "fork:only",
				UserRepositoryTypeFilter.Archived => "archived:true fork:true",
				UserRepositoryTypeFilter.Sponsorable => "is:sponsorable fork:true",
				UserRepositoryTypeFilter.Mirrors => "mirror:true fork:true",
				UserRepositoryTypeFilter.Templates => "template:true fork:true",
				_ => throw new ArgumentOutOfRangeException(nameof(filters.Type), filters.Type, "Unsupported repository type filter."),
			};
			if (typeQualifier is not null)
				terms.Add(typeQualifier);

			if (!string.IsNullOrWhiteSpace(filters.Language))
				terms.Add($"language:{Quote(filters.Language.Trim())}");

			if (filters.Sort == UserRepositorySort.Stars)
				terms.Add("sort:stars-desc");

			return string.Join(' ', terms);
		}

		private static string Quote(string value)
			=> $"\"{value.Replace("\\", "\\\\", StringComparison.Ordinal).Replace("\"", "\\\"", StringComparison.Ordinal)}\"";
	}

	public static class UserRepositoryFilter
	{
		public static IReadOnlyList<Repository> Apply(
			IEnumerable<Repository> repositories,
			StarredRepositoryListFilters filters)
		{
			ArgumentNullException.ThrowIfNull(repositories);
			ArgumentNullException.ThrowIfNull(filters);

			var filtered = repositories.Where(repository => Matches(repository, filters));
			return filters.Sort switch
			{
				StarredRepositorySort.RecentlyStarred => filtered.ToList(),
				StarredRepositorySort.RecentlyActive => filtered
					.OrderByDescending(repository => repository.PushedAt ?? repository.UpdatedAt)
					.ToList(),
				StarredRepositorySort.MostStars => filtered
					.OrderByDescending(repository => repository.StargazerCount)
					.ThenBy(repository => repository.Name, StringComparer.OrdinalIgnoreCase)
					.ToList(),
				_ => throw new ArgumentOutOfRangeException(nameof(filters.Sort), filters.Sort, "Unsupported starred repository sort."),
			};
		}

		private static bool Matches(Repository repository, StarredRepositoryListFilters filters)
		{
			if (!string.IsNullOrWhiteSpace(filters.SearchText)
				&& !repository.Name.Contains(filters.SearchText.Trim(), StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}

			if (!string.IsNullOrWhiteSpace(filters.Language)
				&& !string.Equals(repository.PrimaryLanguage?.Name, filters.Language.Trim(), StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}

			return filters.Type switch
			{
				UserRepositoryTypeFilter.All => true,
				UserRepositoryTypeFilter.Public => !repository.IsPrivate,
				UserRepositoryTypeFilter.Private => repository.IsPrivate,
				UserRepositoryTypeFilter.Sources => !repository.IsFork,
				UserRepositoryTypeFilter.Forks => repository.IsFork,
				UserRepositoryTypeFilter.Archived => repository.IsArchived,
				UserRepositoryTypeFilter.Sponsorable => repository.HasSponsorshipsEnabled,
				UserRepositoryTypeFilter.Mirrors => repository.IsMirror,
				UserRepositoryTypeFilter.Templates => repository.IsTemplate,
				_ => throw new ArgumentOutOfRangeException(nameof(filters.Type), filters.Type, "Unsupported repository type filter."),
			};
		}
	}
}
