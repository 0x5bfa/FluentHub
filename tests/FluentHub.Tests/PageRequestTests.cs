using FluentHub.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class PageRequestTests
{
	[TestMethod]
	public void ForwardSetsOnlyForwardPaginationValues()
	{
		var request = PageRequest.Forward(20, "next-cursor");

		Assert.AreEqual(20, request.First);
		Assert.AreEqual("next-cursor", request.After);
		Assert.IsNull(request.Last);
		Assert.IsNull(request.Before);
	}

	[TestMethod]
	public void BackwardSetsOnlyBackwardPaginationValues()
	{
		var request = PageRequest.Backward(10, "previous-cursor");

		Assert.AreEqual(10, request.Last);
		Assert.AreEqual("previous-cursor", request.Before);
		Assert.IsNull(request.First);
		Assert.IsNull(request.After);
	}

	[TestMethod]
	[DataRow(0)]
	[DataRow(-1)]
	public void ForwardRejectsNonPositiveCounts(int count)
		=> Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => PageRequest.Forward(count));

	[TestMethod]
	[DataRow(0)]
	[DataRow(-1)]
	public void BackwardRejectsNonPositiveCounts(int count)
		=> Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => PageRequest.Backward(count));
}
