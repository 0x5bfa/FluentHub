using FluentHub.Core.Infrastructure.GitHub.Authorization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class AuthorizationServiceTests
{
	[TestMethod]
	public async Task WaitForDeviceAccessTokenRejectsExpiredAuthorizationWithoutNetworkAccess()
	{
		var service = new AuthorizationService();
		var authorization = new DeviceAuthorizationResponse
		{
			DeviceCode = "device-code",
			ExpiresIn = 0,
		};

		await Assert.ThrowsExactlyAsync<TimeoutException>(() =>
			service.WaitForDeviceAccessTokenAsync(authorization));
	}
}
