using FluentHub.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace FluentHub.Services.OctokitEx
{
    public class Markdown
    {
        public async Task<string> FormatRenderedMarkdownToHtml(string renderedString)
        {
            ApplicationTheme appTheme = Application.Current.RequestedTheme;
            StorageFile template
                = await StorageFile.GetFileFromApplicationUriAsync(
                    new Uri("ms-appx:///Assets/WebView/RenderedMarkdownTemplate.html"));
            StorageFile style;
            string templateText = await Windows.Storage.FileIO.ReadTextAsync(template);
            string result;

            if (appTheme == ApplicationTheme.Light)
            {
                style = await StorageFile.GetFileFromApplicationUriAsync(
                    new Uri("ms-appx:///Assets/WebView/github-markdown-light.css"));
            }
            else // ApplicationTheme.Dark
            {
                style = await StorageFile.GetFileFromApplicationUriAsync(
                    new Uri("ms-appx:///Assets/WebView/github-markdown-dark.css"));
            }

            string styleText = await Windows.Storage.FileIO.ReadTextAsync(style);

            result = templateText.Replace("{0}", styleText);
            return result = result.Replace("{1}", renderedString);
        }

        public string FixRelativeLinkInHtml(string renderedString, string missedPath)
        {
            var baseUri = new Uri("https://raw.githubusercontent.com/fluenthub-uwp/FluentHub/blob/main/");

            var pattern = @"(?<name>src|href)=""(?<value>[^""]*)""";

            var matchEvaluator = new MatchEvaluator(
                match =>
                {
                    var value = match.Groups["value"].Value;
                    Uri uri;

                    if (Uri.TryCreate(baseUri, value, out uri))
                    {
                        var name = match.Groups["name"].Value;
                        return string.Format("{0}=\"{1}\"", name, uri.AbsoluteUri);
                    }

                    return null;
                });

            var adjustedHtml = Regex.Replace(renderedString, pattern, matchEvaluator);

            return adjustedHtml;
        }
    }
}
