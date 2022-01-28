using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace FluentHub.ViewModels
{
    public class SettingsViewModel : SettingsManager
    {
        public bool SetupCompleted
        {
            get => Get(nameof(SetupCompleted), false);
            set => Set(nameof(SetupCompleted), value);
        }

        public bool SetupProgress
        {
            get => Get(nameof(SetupProgress), false);
            set => Set(nameof(SetupProgress), value);
        }

        public string AccessToken
        {
            get => Get(nameof(AccessToken), "");
            set => Set(nameof(AccessToken), value);
        }

        public ApplicationTheme AppTheme
        {
            get => Get(nameof(AppTheme), Application.Current.RequestedTheme);
            set => Set(nameof(AppTheme), value);
        }

        public string LastOpenedPageFrame
        {
            get => Get(nameof(LastOpenedPageFrame), nameof(Views.UserHomePage));
            set => Set(nameof(LastOpenedPageFrame), value);
        }

        public string AppVersion
        {
            get => Get(nameof(AppVersion), "Unknown version");
            set => Set(nameof(AppVersion), value);
        }
    }
}
