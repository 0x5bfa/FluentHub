using Octokit;
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
        private User user = new();
        public User User
        {
            get => user;
            private set => SetProperty(ref user, value);
        }

        public async Task<bool> GetUser(string login)
        {
            try
            {
                User = await App.Client.User.Get(login);
                return true;
            }
            catch (ApiException apiEx)
            {
                Log.Error(apiEx, apiEx.Message);
                return false;
            }
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
