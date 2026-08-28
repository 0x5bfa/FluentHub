// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Common;

public enum AppErrorKind
{
	Unknown,
	Validation,
	Authentication,
	Authorization,
	NotFound,
	Conflict,
	RateLimited,
	Network,
	Cancelled,
}

public sealed record AppError(
	AppErrorKind Kind,
	string Code,
	string Message,
	bool IsTransient = false)
{
	public static AppError Unexpected(string message)
		=> new(AppErrorKind.Unknown, "unexpected", message);
}
