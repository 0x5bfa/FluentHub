using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Users
{
    // Warning: There is no notification API in Octokit.GraphQL.NET (even Octokit.GraphQL)
    public class NotificationQueries
    {
        public async Task<Models.Notification> GetAll()
        {
            //NotificationsRequest request = new NotificationsRequest();
            //request.All = true;
            //ApiOptions options = new() { PageCount = 1, PageSize = 50, StartPage = 1 };
            //var notifications = await App.Client.Activity.Notifications.GetAllForCurrent(request, options);

            //foreach (var item in notifications)
            //{
            //    NotificationButtonBlockViewModel viewModel = new();

            //    viewModel.NotificationItem = item;
            //    viewModel.UpdatedAtHumanized = DateTimeOffset.Parse(item.UpdatedAt).Humanize();
            //    viewModel.NameWithOwner = item.Repository.Owner.Login + " / " + item.Repository.Name;

            //    NotificationItems.Add(viewModel);
            //}

            return null;
        }
    }
}
