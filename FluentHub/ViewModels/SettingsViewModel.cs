using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

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
    }
}
