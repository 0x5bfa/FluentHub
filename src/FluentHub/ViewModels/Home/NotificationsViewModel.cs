using FluentHub.ViewModels.UserControls.ButtonBlocks;
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

namespace FluentHub.ViewModels.Home
{
    public class NotificationsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<NotificationButtonBlockViewModel> NotificationItems { get; set; } = new();

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public async Task GetNotifications()
        {
            IsActive = true;

            NotificationsRequest request = new NotificationsRequest();
            request.All = true;
            ApiOptions options = new() { PageCount = 1, PageSize = 50, StartPage = 2 };
            var notifications = await App.Client.Activity.Notifications.GetAllForCurrent(request, options);

            foreach (var item in notifications)
            {
                NotificationButtonBlockViewModel viewModel = new();

                viewModel.NotificationItem = item;
                viewModel.UpdatedAtHumanized = DateTimeOffset.Parse(item.UpdatedAt).Humanize();
                viewModel.NameWithOwner = item.Repository.Owner.Login + " / " + item.Repository.Name;

                NotificationItems.Add(viewModel);
            }

            IsActive = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
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
