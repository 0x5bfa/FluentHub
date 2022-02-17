using FluentHub.Helpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace FluentHub.Services.OctokitEx
{
    public class Markdown
    {
        ApplicationTheme appTheme = Application.Current.RequestedTheme;

        public async Task<string> GetHtml(string markdown, string missedPath)
        {
            if (string.IsNullOrEmpty(markdown))
            {
                return null;
            }

            StorageFile indexFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/WebView/index.html"));
            var indexHtml = await FileIO.ReadTextAsync(indexFile);

            var htmlBody = Get(markdown);

            htmlBody = AddLineBreak(htmlBody);

            string fullHtml
                = ((string)indexHtml!.Clone())
                .Replace("{{renderTheme}}", appTheme.ToString().ToLower())
                .Replace("{{htmlBody}}", htmlBody);

            fullHtml = FixRelativeLink(fullHtml, missedPath);

            return fullHtml;
        }

        public string AddLineBreak(string body)
        {
            string formattedBody = body;

            formattedBody.Replace(@"</strong>", "</strong>\n<br>");

            return formattedBody;
        }

        public string FixRelativeLink(string renderedString, string missedPath)
        {
            var baseUri = new Uri(missedPath);

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

        public string Get(string markdown)
        {
            string url = "https://api.github.com/markdown/raw";

            try
            {
                WebClient wc = new WebClient();

                wc.Headers["Content-Type"] = "text/plain";
                wc.Headers["User-agent"] = "FluentHub";
                wc.Headers["Authorization"] = "token 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";
                //wc.Headers["Mode"] = "GFM";

                var resData = wc.UploadData(url, "POST", Encoding.UTF8.GetBytes(markdown));
                wc.Dispose();

                string data = Encoding.UTF8.GetString(resData);

                return data;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return null;
            }
        }
    }
}
