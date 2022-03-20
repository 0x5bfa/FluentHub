using FluentHub.OctokitEx.Models;
using FluentHub.OctokitEx.Queries.User;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FluentHub.ViewModels.Users
{
    public class ProfilePageViewModel : INotifyPropertyChanged
    {
        private UserOverviewItem userItem;
        public UserOverviewItem UserItem
        {
            get => userItem;
            private set => SetProperty(ref userItem, value);
        }

        private bool isNotViewer;
        public bool IsNotViewer
        {
            get => isNotViewer;
            set => SetProperty(ref isNotViewer, value);
        }

        public async Task GetUser(string login)
        {
            UserQueries queries = new();
            UserItem = await queries.GetOverview(login);

            if (!UserItem.IsViewer) IsNotViewer = true;
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
