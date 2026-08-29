using FluentHub.Core.Application.Navigation;

namespace FluentHub.Tests;

[TestClass]
public sealed class NavigationJournalTests
{
	[TestMethod]
	public void Navigate_Back_And_Forward_Use_Route_Only_History()
	{
		var journal = new NavigationJournal<AppRoute>();
		var dashboard = new DashboardRoute();
		var notifications = new NotificationsRoute();

		journal.Navigate(dashboard);
		journal.Navigate(notifications);

		Assert.IsTrue(journal.TryGoBack(out var backRoute));
		Assert.AreEqual(dashboard, backRoute);
		Assert.IsTrue(journal.TryGoForward(out var forwardRoute));
		Assert.AreEqual(notifications, forwardRoute);
	}

	[TestMethod]
	public void Navigate_After_Back_Removes_Forward_Branch()
	{
		var journal = new NavigationJournal<AppRoute>();
		journal.Navigate(new DashboardRoute());
		journal.Navigate(new NotificationsRoute());
		journal.TryGoBack(out _);

		var replacement = new AppSettingsRoute();
		journal.Navigate(replacement);

		Assert.AreEqual(2, journal.Entries.Count);
		Assert.AreEqual(replacement, journal.Current);
		Assert.IsFalse(journal.CanGoForward);
	}

	[TestMethod]
	public void RepositorySlug_Rejects_Empty_Identifiers()
	{
		Assert.ThrowsExactly<ArgumentException>(() => new RepositorySlug("", "repo"));
		Assert.ThrowsExactly<ArgumentException>(() => new RepositorySlug("owner", " "));
	}
}
