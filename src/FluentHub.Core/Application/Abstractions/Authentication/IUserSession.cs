// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Abstractions.Authentication;

public interface IUserSession
{
	bool IsAuthenticated { get; }

	void SwitchAccount(string accessToken);
}
