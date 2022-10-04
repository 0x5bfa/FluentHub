using System;

namespace FluentHub.Uwp.Models
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
