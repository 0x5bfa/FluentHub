// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Abstractions.Authentication;

public interface IUserSession
{
	bool IsAuthenticated { get; }

	void SwitchAccount(string accessToken);
}
