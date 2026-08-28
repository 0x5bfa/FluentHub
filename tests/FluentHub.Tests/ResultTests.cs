using FluentHub.Core.Application.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class ResultTests
{
	[TestMethod]
	public void SuccessExposesValue()
	{
		var result = Result<string>.Success("value");

		Assert.IsTrue(result.IsSuccess);
		Assert.AreEqual("value", result.Value);
		Assert.IsNull(result.Error);
	}

	[TestMethod]
	public void FailureExposesErrorAndRejectsValueAccess()
	{
		var error = new AppError(AppErrorKind.Network, "network", "Request failed.", IsTransient: true);
		var result = Result<string>.Failure(error);

		Assert.IsFalse(result.IsSuccess);
		Assert.AreSame(error, result.Error);
		Assert.ThrowsExactly<InvalidOperationException>(() => _ = result.Value);
	}
}
