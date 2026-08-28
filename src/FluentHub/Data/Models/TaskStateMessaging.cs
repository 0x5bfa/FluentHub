// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Models
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
