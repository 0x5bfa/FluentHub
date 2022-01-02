using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FluentHub.Services
{
    class AppSettingsService
    {
        private readonly ApplicationDataContainer settingsContainer;

        public AppSettingsService()
        {
            settingsContainer = ApplicationData.Current.LocalSettings;
        }

        public T GetValue<T>(string key)
        {
            if (settingsContainer.Values.ContainsKey(key))
            {
                try
                {
                    return (T)settingsContainer.Values[key];
                }
                catch (Exception e)
                {
                    return default(T);
                }
            }

            return default(T);
        }

        public void RemoveValue(string key)
        {
            if (settingsContainer.Values.ContainsKey(key))
            {
                settingsContainer.Values.Remove(key);
            }
        }

        public void SetValue<T>(string key, T value)
        {
            settingsContainer.Values[key] = value;
        }
    }
}
