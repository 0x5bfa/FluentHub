using Humanizer;
using FluentHub.ViewModels.UserControls.Blocks;
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
    public class ActivitiesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ActivityBlockViewModel> EventItems { get; set; } = new();

        public async Task GetAllActivityForCurrent(string login)
        {
            IsActive = true;

            // It takes too much time. but there is no event API in Octokit.GraphQl.NET.
            ApiOptions options = new() { PageCount = 1, PageSize = 30, StartPage = 1 };
            var events = await App.Client.Activity.Events.GetAllUserReceived(login, options);

            foreach (var item in events)
            {
                ActivityBlockViewModel viewModel = new();

                switch (item.Type)
                {
                    case "ForkEvent":
                        viewModel.IsForkEvent = true;
                        break;
                    case "WatchEvent":
                        viewModel.IsWatchEvent = true;
                        break;
                    case "PushEvent":
                        viewModel.IsPushEvent = true;
                        break;
                }

                viewModel.FullPayload = item;
                viewModel.UpdatedAtHumanized = item.CreatedAt.Humanize();

                EventItems.Add(viewModel);
            }

            IsActive = false;
        }

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

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
