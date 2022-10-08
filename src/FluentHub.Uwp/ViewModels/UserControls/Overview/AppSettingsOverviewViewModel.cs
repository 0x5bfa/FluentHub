using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.UserControls.Overview
{
    public class AppSettingsOverviewViewModel : ObservableObject
    {
        #region Fields and Properties
        private User _user;
        public User User { get => _user; set => SetProperty(ref _user, value); }

        public static User StoredUser;

        private string _selectedTag;
        public string SelectedTag { get => _selectedTag; set => SetProperty(ref _selectedTag, value); }
        #endregion
    }
}
