using System.Net;
using System.Text.RegularExpressions;

namespace FluentHub.Octokit.Queries.Repositories
{
    public class MarkdownQueries
    {
        public string GetHtml(string index, string markdown, string missedPath, string theme, bool isHtml)
        {
            // Get rwa html string
            string body = isHtml ? markdown : GetRawHtml(markdown);

            //body = AddMissedLineBreaks(body);

            string html
                = ((string)index!.Clone())
                .Replace("{{renderTheme}}", theme)
                .Replace("{{htmlBody}}", body);

            html = CorrectRelativePaths(html, missedPath);
            return html;
        }

        private string GetRawHtml(string markdown)
        {
            try
            {
                WebClient wc = new WebClient();

                wc.Headers["Content-Type"] = "text/plain";
                wc.Headers["User-agent"] = "FluentHub";
                wc.Headers["Authorization"] = "token " + App.AccessToken;
                wc.Headers["Mode"] = "GFM";

                var response = wc.UploadData("https://api.github.com/markdown/raw", "POST", Encoding.UTF8.GetBytes(markdown));
                wc.Dispose();

                string data = Encoding.UTF8.GetString(response);

                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private string CorrectRelativePaths(string html, string missedPath)
        {
            var baseUri = new Uri(missedPath);

            var pattern = @"(?<name>src|href)=""(?<value>[^""]*)""";

            var matchEvaluator = new MatchEvaluator(
                match =>
                {
                    var value = match.Groups["value"].Value;

                    // Remove the first character slash
                    if (value[0] == '/') value = value.Remove(0, 1);

                    Uri uri = new Uri(baseUri, value);

                    var name = match.Groups["name"].Value;
                    return string.Format("{0}=\"{1}\"", name, uri.AbsoluteUri);
                });

            var correctedHtml = Regex.Replace(html, pattern, matchEvaluator);

            return correctedHtml;
        }

        private string AddMissedLineBreaks(string html)
        {
            return html.Replace(@"</strong>", "</strong>\n</br>");
        }
    }
}