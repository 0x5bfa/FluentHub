using FluentHub.Octokit.Queries;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Users
{
    public class FollowersViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UserButtonBlockViewModel> FollowersItems = new();

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public async Task GetFollowersList(string login)
        {
            IsActive = true;

            FollowersQueries client = new();
            var followers = await client.GetOverview(login);

            foreach (var user in followers)
            {
                UserButtonBlockViewModel viewModel = new() { User = user };
                FollowersItems.Add(viewModel);
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
