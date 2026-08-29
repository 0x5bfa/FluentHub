using FluentHub.Core.Application.Models;
using FluentHub.Core.Application.Models;

namespace FluentHub.Core.Application
{
	public static class PinnedRepositoryService
	{
		public static IReadOnlyList<PinnableRepositoryItem> CreateItems(
			IEnumerable<Repository> pinnableRepositories,
			IEnumerable<Repository> pinnedRepositories)
		{
			ArgumentNullException.ThrowIfNull(pinnableRepositories);
			ArgumentNullException.ThrowIfNull(pinnedRepositories);

			var pinnedNames = pinnedRepositories
				.Select(repository => repository.NameWithOwner)
				.ToHashSet(StringComparer.OrdinalIgnoreCase);

			return pinnableRepositories
				.Select(repository => new PinnableRepositoryItem
				{
					IsPinned = pinnedNames.Contains(repository.NameWithOwner),
					PinnableItem = repository,
				})
				.ToList();
		}
	}
}
