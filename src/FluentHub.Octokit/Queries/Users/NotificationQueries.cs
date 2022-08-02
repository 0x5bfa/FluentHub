namespace FluentHub.Octokit.Queries.Users
{
    public class NotificationQueries
    {
        public async Task<List<Notification>> GetAllAsync(OctokitOriginal.NotificationsRequest request = null, OctokitOriginal.ApiOptions options = null)
        {
            var response = await App.Client.Activity.Notifications.GetAllForCurrent(request, options);

            Wrappers.NotificationWrapper wrapper = new();
            var notifications = await wrapper.WrapAsync(response);

            return notifications;
        }

        public async Task<int> GetUnreadCount()
        {
            OctokitOriginal.NotificationsRequest request = new()
            {
                All = true,
            };

            OctokitOriginal.ApiOptions options = new()
            {
                PageCount = 1,
                PageSize = 50,
                StartPage = 1
            };

            // Even if there are more than 50 unread items, this method will only count up to a maximum of 50.
            var response = await App.Client.Activity.Notifications.GetAllForCurrent(request, options);

            int unreadCount = 0;
            foreach (var indivisual in response)
            {
                if (indivisual.Unread) unreadCount++;
            }

            return unreadCount;
        }
    }
}
