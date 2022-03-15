using FluentHub.Models.Items;
using FluentHub.ViewModels.UserControls.Blocks;
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
            var events = await App.Client.Activity.Events.GetAllUserReceived(login);

            foreach (var item in events)
            {
                // Allowed event types: releases, fork repos, star(watch) repos/users, push commits
                if (item.Type != "ForkEvent" && item.Type != "WatchEvent")
                {
                    continue;
                }

                ActivityBlockViewModel viewModel = new();

                // Event type
                _ = item.Type switch
                {
                    "ForkEvent" => viewModel.IsForkEvent = true,
                    "WatchEvent" => viewModel.IsWatchEvent = true,
                };

                viewModel.AvatarUrl = item.Actor.AvatarUrl;
                viewModel.Actor = item.Actor.Login;
                viewModel.FullPayload = item;

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
