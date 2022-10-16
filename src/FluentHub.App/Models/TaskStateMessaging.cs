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
