using FluentHub.Models.Items;
using Octokit;
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
    public class ActivityViewModel : INotifyPropertyChanged
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

        public async Task GetAllActivityForCurrent()
        {
            IsActive = true;

            var events = await App.Client.Activity.Events.GetAllUserReceived($"{App.SignedInUserName}");

            foreach (var item in events)
            {
                if (item.Type != "ForkEvent"
                    && item.Type != "PushEvent"
                    && item.Type != "ReleaseEvent"
                    && item.Type != "WatchEvent")
                {
                    continue;
                }
            }

            IsActive = false;
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
