using Windows.ApplicationModel.Background;

namespace FluentHub.Core
{
    public class BackgroundTasks : IBackgroundTask
    {
        public void Configure()
        {
        }

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            deferral.Complete();
        }
    }
}