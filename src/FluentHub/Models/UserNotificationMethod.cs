using System;

namespace FluentHub.Models
{
    [Serializable]
    [Flags]
    public enum UserNotificationMethod : uint
    {
        None = 0,
        InApp = 1,
        Push = 2,
        All = InApp | Push
    }
}