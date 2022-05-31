using FluentHub.Octokit.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FluentHub.ViewModels.UserControls.ButtonBlocks
{
    public class AccountButtonBlockViewModel : INotifyPropertyChanged
    {
        private User user;
        public User User
        {
            get => user;
            set
            {
                SetProperty(ref user, value);
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