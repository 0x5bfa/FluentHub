using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Users;
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
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace FluentHub.ViewModels.Users
{
    public class ProfilePageViewModel : ObservableObject
    {
        public ProfilePageViewModel(ILogger logger = null)
        {
            _logger = logger;

            LoadUserCommand = new AsyncRelayCommand<string>(LoadUserAsync, CanLoadUser);
        }

        private readonly ILogger _logger;
        private User _userItem;
        private bool _isNotViewer;
        public User UserItem
        {
            get => _userItem;
            private set => SetProperty(ref _userItem, value);
        }
        public bool IsNotViewer
        {
            get => _isNotViewer;
            set => SetProperty(ref _isNotViewer, value);
        }

        public IAsyncRelayCommand LoadUserCommand { get; }

        private bool CanLoadUser(string username) => !string.IsNullOrEmpty(username);

        private async Task LoadUserAsync(string username)
        {
            try
            {
                UserQueries queries = new();
                UserItem = await queries.GetOverview(username);

                if (!UserItem.IsViewer)
                    IsNotViewer = true;
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to load user.");
                UserItem = new User();
                IsNotViewer = false;
                throw;
            }
        }
    }
}