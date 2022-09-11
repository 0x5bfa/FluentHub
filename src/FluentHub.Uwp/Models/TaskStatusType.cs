using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Uwp.Models
{
    public enum TaskStatusType
    {
        IsStarted,

        IsInProgress,

        IsFaulted,

        IsCompleted,

        IsCompletedSuccessfully,

        Unknown,
    }
}
