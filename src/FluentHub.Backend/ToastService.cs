using Microsoft.Toolkit;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Globalization;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace FluentHub.Backend
{
    public class ToastService
    {
        public ToastService(ILogger logger = null) => _logger = logger;

        private readonly ILogger _logger;

        public void ShowToastNotification(string title,
                                          string text,
                                          string activationArgs = "",
                                          string appLogoOverrideImage = null,
                                          string heroImage = null,
                                          string inlineImage = null)
        {
            try
            {
                var builder = new ToastContentBuilder()
                                           .SetToastScenario(ToastScenario.Default)
                                           .AddToastActivationInfo(activationArgs, ToastActivationType.Foreground)
                                           .AddText(title.Truncate(50, true))
                                           .AddText(text.Truncate(100, true));

                /*builder.Content.Audio = new ToastAudio()
                {
                    Src = new Uri("ms-appx:///Assets/notification-sound.mp3")
                };*/

                if (Uri.TryCreate(appLogoOverrideImage, UriKind.RelativeOrAbsolute, out var uri))
                    builder.AddAppLogoOverride(uri);
                if (Uri.TryCreate(heroImage, UriKind.RelativeOrAbsolute, out uri))
                    builder.AddHeroImage(uri);
                if (Uri.TryCreate(inlineImage, UriKind.RelativeOrAbsolute, out uri))
                    builder.AddInlineImage(uri);

                ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(builder.Content.GetXml()));
            }
            catch (Exception e)
            {
                _logger?.Error(e);
                throw;
            }
        }

        public void UpdateBadgeGlyph(BadgeGlyphType glyphType, int? number)
        {
            XmlDocument badgeXml;
            string badgeGlyphValue;
            if (glyphType == BadgeGlyphType.Number)
            {
                if (number is null)
                    throw new ArgumentNullException(nameof(number));
                // Get the blank badge XML payload for a badge glyph
                badgeXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
                badgeGlyphValue = number.ToString();
            }
            else
            {
                badgeXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeGlyph);
                badgeGlyphValue = glyphType.ToString().ToLower();
            }

            // Set the value of the badge in the XML to our glyph value
            XmlElement badgeElement = (XmlElement)badgeXml.SelectSingleNode("/badge");
            badgeElement.SetAttribute("value", badgeGlyphValue);

            // Create the badge notification
            BadgeNotification badge = new BadgeNotification(badgeXml);

            // Create the badge updater for the application
            BadgeUpdater badgeUpdater = BadgeUpdateManager.CreateBadgeUpdaterForApplication();

            // And update the badge
            badgeUpdater.Update(badge);
        }

        public void ShowToastNotificationWithProgress(string tag,
                                                       string group,
                                                       string title,
                                                       string progressTitle,
                                                       string leftText,
                                                       string rightText,
                                                       double progress)
        {
            // Define a tag (and optionally a group) to uniquely identify the notification, in order update the notification data later;
            //string tag = "weekly-playlist";
            //string group = "downloads";

            // Construct the toast content with data bound fields
            var content = new ToastContent()
            {
                /*Audio = new ToastAudio()
                {
                    Src = new Uri("ms-appx:///Assets/notification-sound.mp3")
                },*/
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = title
                            },

                            new AdaptiveProgressBar()
                            {
                                Title = progressTitle,
                                Value = new BindableProgressBarValue("progress"),
                                Status = new BindableString("leftText"),
                                ValueStringOverride = new BindableString("rightText")
                            }
                        }
                    }
                }
            };

            // Generate the toast notification
            var toast = new ToastNotification(content.GetXml())
            {
                // Assign the tag and group
                Tag = tag,
                Group = group,

                // Assign initial NotificationData values
                // Values must be of type string
                Data = new NotificationData()
            };
            toast.Data.Values["progress"] = progress.ToString(new CultureInfo("en-US"));
            toast.Data.Values["leftText"] = leftText;
            toast.Data.Values["rightText"] = rightText;

            // Provide sequence number to prevent out-of-order updates, or assign 0 to indicate "always update"
            toast.Data.SequenceNumber = 1;

            // Show the toast notification to the user
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        public void UpdateToastWithProgress(string tag,
                                                   string group,
                                                   string leftText,
                                                   string rightText,
                                                   double progress,
                                                   uint sequenceNumber)
        {
            // Construct a NotificationData object;
            //string tag = "weekly-playlist";
            //string group = "downloads";

            // Create NotificationData and make sure the sequence number is incremented
            // since last update, or assign 0 for updating regardless of order
            var data = new NotificationData
            {
                SequenceNumber = sequenceNumber
            };

            // Assign new values
            // Note that you only need to assign values that changed. In this example
            // we don't assign progressStatus since we don't need to change it
            data.Values["progress"] = progress.ToString(new CultureInfo("en-US"));
            data.Values["leftText"] = leftText;
            data.Values["rightText"] = rightText;

            // Update the existing notification's data by using tag/group
            ToastNotificationManager.CreateToastNotifier().Update(data, tag, group);
        }
    }
}
