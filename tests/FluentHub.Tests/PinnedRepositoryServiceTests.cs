using FluentHub.Core.Application;
using FluentHub.Core.Application.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class PinnedRepositoryServiceTests
{
	[TestMethod]
	public void CreateItemsMarksPinnedRepositoriesIgnoringCase()
	{
		var pinnableRepositories = new[]
		{
			new Repository { NameWithOwner = "owner/first" },
			new Repository { NameWithOwner = "owner/second" },
		};
		var pinnedRepositories = new[]
		{
			new Repository { NameWithOwner = "OWNER/SECOND" },
		};

		var items = PinnedRepositoryService.CreateItems(pinnableRepositories, pinnedRepositories);

		Assert.HasCount(2, items);
		Assert.IsFalse(items[0].IsPinned);
		Assert.IsTrue(items[1].IsPinned);
	}
}
