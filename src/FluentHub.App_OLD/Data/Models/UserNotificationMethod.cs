// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Models
{
	[Serializable]
	[Flags]
	public enum UserNotificationMethod : uint
	{
		None = 0,

		InApp = 1,

		Toast = 2,

		All = InApp | Toast
	}
}
