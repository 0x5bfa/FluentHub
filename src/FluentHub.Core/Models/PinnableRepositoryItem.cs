using FluentHub.Core.Contracts;

namespace FluentHub.Core.Models
{
	public sealed class PinnableRepositoryItem
	{
		public bool IsPinned { get; set; }

		public Repository PinnableItem { get; set; } = default!;
	}
}
