using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using Windows.Storage;

namespace FluentHub.Uwp.ViewModels
{
    public class SettingsManager
    {
        public SettingsManager(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;
        #endregion

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
                _logger?.Error("SettingsManager.Get()", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
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
                _logger?.Error("SettingsManager.Set()", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
    }
}
