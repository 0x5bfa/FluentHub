using FluentHub.Core.Authorization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class AuthorizationServiceTests
{
	[TestMethod]
	public async Task WaitForDeviceAccessTokenRejectsExpiredAuthorizationWithoutNetworkAccess()
	{
		var service = new AuthorizationService();
		var secrets = new OctokitSecrets { ClientId = "client-id" };
		var authorization = new DeviceAuthorizationResponse
		{
			DeviceCode = "device-code",
			ExpiresIn = 0,
		};

		await Assert.ThrowsExactlyAsync<TimeoutException>(() =>
			service.WaitForDeviceAccessTokenAsync(secrets, authorization));
	}
}
