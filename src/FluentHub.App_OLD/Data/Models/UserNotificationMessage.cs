// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Models
{
	public class UserNotificationMessage
	{
		public UserNotificationMessage(string title, string message = "", UserNotificationType type = UserNotificationType.Info, UserNotificationMethod method = UserNotificationMethod.InApp)
		{
			Title = title;
			Message = message ?? "";
			Type = type;
			Method = method;
		}

		public UserNotificationType Type { get; }
		public UserNotificationMethod Method { get; }
		public string Title { get; }
		public string Message { get; }
	}
}
