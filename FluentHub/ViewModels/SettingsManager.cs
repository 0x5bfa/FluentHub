using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FluentHub.ViewModels
{
    public class SettingsManager
    {
        public T Get<T>(string setting, T defaultValue)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            if (localSettings.Values[setting] == null)
            {
                localSettings.Values[setting] = defaultValue;
            }

            object val = localSettings.Values[setting];

            // Check type mismatch
            if (!(val is T))
            {
                throw new ArgumentException("Type mismatch for \"" + setting + "\" in local store. Got " + val.GetType());
            }

            return (T)val;
        }

        public void Set<T>(string setting, T newValue)
        {
            // if types don't match, this'll throw an exception
            _ = Get(setting, newValue);

            // Set app settings
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[setting] = newValue;

            return;
        }
    }
}
