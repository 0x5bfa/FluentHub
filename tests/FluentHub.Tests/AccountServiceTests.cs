using FluentHub.Core.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class AccountServiceTests
{
	[TestMethod]
	public void AddAccountDeduplicatesLoginsIgnoringCase()
	{
		var store = new TestAccountStore { SignedInUserLogins = "octocat,MONALISA" };
		var service = new AccountService(store);

		service.AddAccount("OctoCat");

		Assert.AreEqual("MONALISA,OctoCat", store.SignedInUserLogins);
	}

	[TestMethod]
	public void RemoveAccountRemovesLoginIgnoringCase()
	{
		var store = new TestAccountStore { SignedInUserLogins = "octocat,monalisa" };
		var service = new AccountService(store);

		service.RemoveAccount("MONALISA");

		Assert.AreEqual("octocat", store.SignedInUserLogins);
		Assert.IsFalse(service.IsAlreadySignedIn("monalisa"));
	}

	private sealed class TestAccountStore : IAccountStore
	{
		public string SignedInUserLogins { get; set; } = string.Empty;
	}
}
