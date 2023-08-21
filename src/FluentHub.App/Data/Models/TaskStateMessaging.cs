// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Models
{
	public class TaskStateMessaging
	{
		public TaskStateMessaging(TaskStatusType statusType = TaskStatusType.Unknown)
		{
			StatusType = statusType;
		}

		public TaskStatusType StatusType { get; }
	}
}
