using Humanizer;
using Octokit;
using FluentHub.Models.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FluentHub.Helpers;

namespace FluentHub.ViewModels.Home
{
    public class NotificationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<NotificationListItem> _items = new ObservableCollection<NotificationListItem>();
        public ObservableCollection<NotificationListItem> Items
        {
            get => _items;
            private set
            {
                _items = value;
                NotifyPropertyChanged(nameof(Items));
            }
        }

        public async Task GetNotifications()
        {
            IsActive = true;

            NotificationsRequest request = new NotificationsRequest();
            request.All = true;

            var notifications = await App.Client.Activity.Notifications.GetAllForCurrent(request);

            foreach (var item in notifications)
            {
                NotificationListItem listItem = new NotificationListItem();

                //listItem.SubjectId = GetSubjectIdFromUrl(item.Subject.Url);
                listItem.SubjectTitle = item.Subject.Title;
                listItem.SubjectWithOwnerName = item.Repository.Owner.Login + " / " + item.Repository.Name;
                listItem.Unread = item.Unread ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
                listItem.FormattedNotifiedReason = item.Reason;
                var parsedUpdatedAt = DateTimeOffset.Parse(item.UpdatedAt);
                listItem.FormattedNotifiedDate = parsedUpdatedAt.Humanize();
                Items.Add(listItem);
            }

            IsActive = false;
        }

        private int GetSubjectIdFromUrl(string url)
        {
            if (url == null)
            {
                return 0;
            }

            var splitedUrl = url.Split("/");
            string id = splitedUrl[splitedUrl.Count() - 1];

            return Convert.ToInt32(id);
        }


        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
