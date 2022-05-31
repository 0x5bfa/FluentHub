using Serilog;
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
        /// <summary>
        /// Gets value with the specified name from the app local settings container.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="setting">Name of value</param>
        /// <param name="defaultValue">Default value for when value does not exist</param>
        /// <returns></returns>
        protected T Get<T>(string setting, T defaultValue)
        {
            try
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

                // Initialize
                if (localSettings.Values[setting] == null)
                    localSettings.Values[setting] = defaultValue;

                object val = localSettings.Values[setting];
                return (T)val;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return default(T);
            }
        }

        /// <summary>
        /// Sets value with the specified name to the app local settings container.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="setting">Name of value to add new value</param>
        /// <param name="newValue">Value to be added</param>
        protected void Set<T>(string setting, T newValue)
        {
            try // Type mismatch will never occur
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values[setting] = newValue;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }
    }
}
