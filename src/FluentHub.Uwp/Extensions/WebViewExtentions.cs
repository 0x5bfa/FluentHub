using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Extensions
{
    public static class WebViewExtentions
    {
        public static async Task HandleResize(this WebView2 webView2)
        {
            try
            {
                var heightString = await webView2.ExecuteScriptAsync("document.getElementById('container').scrollHeight.toString()");

                if (int.TryParse(heightString, out int height))
                {
                    webView2.Height = height;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
            }
        }
    }
}
