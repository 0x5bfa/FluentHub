// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Models
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
