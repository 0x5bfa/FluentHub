using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace FluentHub.Helpers
{
    public class MentionHelpers
    {
        /*
        public async Task<Uri> GetUserUrlFromMention(string mention)
        {
            try
            {
                if (mention.IndexOf("@") == 0) // "@fluenthub-uwp"
                {
                    string rawMentionText = mention.Replace("@", "");

                    // Search for the existence of a user or organization.
                    // If it does not exist, 'Not Found' exception will be thrown.
                    var userCompany = await App.Client.User.Get(rawMentionText);
                    var org = await App.Client.Organization.Get(rawMentionText);

                    string navigateUrl = App.DefaultHost + "/" + rawMentionText;
                    return new UriBuilder(navigateUrl).Uri;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public Task GetTextBlock(string raw, ref TextBlock textBlock)
        {
            return GetTextBlockAsync(raw, textBlock);
        }

        public async Task GetTextBlockAsync(string raw, TextBlock textBlock)
        {
            var splitWithSpace = raw.Split(" ");
            textBlock.Inlines.Clear();

            foreach (var item in splitWithSpace)
            {
                var url = await GetUserUrlFromMention(item);

                if (url == null)
                {
                    textBlock.Inlines.Add(new Run() { Text = item });
                }
                else
                {
                    var link = new Hyperlink { NavigateUri = url };

                    link.Inlines.Add(new Run() { Text = item });

                    link.UnderlineStyle = UnderlineStyle.None;
                    link.FontWeight = FontWeights.SemiBold;

                    textBlock.Inlines.Add(link);
                }

                textBlock.Inlines.Add(new Run() { Text = " " });
            }
        }
        */
    }
}
