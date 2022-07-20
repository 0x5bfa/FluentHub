namespace FluentHub.Uwp.Models
{
    public class LoadingMessaging
    {
        public LoadingMessaging(bool isLoading)
        {
            IsLoading = isLoading;
        }

        public bool IsLoading { get; }
    }
}
