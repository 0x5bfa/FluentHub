using FluentHub.Core.Application.Models;

namespace FluentHub.Core.Application.Models
{
	public sealed class PinnableRepositoryItem
	{
		public bool IsPinned { get; set; }

		public Repository PinnableItem { get; set; } = default!;
	}
}
