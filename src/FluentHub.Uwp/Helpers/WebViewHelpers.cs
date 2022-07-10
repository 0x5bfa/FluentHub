using Windows.UI.Xaml.Controls;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Helpers
{
    public class WebViewHelpers
    {
        private static readonly string SetBodyOverFlowHiddenString = @"function SetBodyOverFlowHidden() { document.body.style.overflow = 'hidden'; } SetBodyOverFlowHidden();";

        public static async Task DisableWebViewVerticalScrollingAsync(WebView webView)
        {
            _ = await webView.InvokeScriptAsync("eval", new string[] { SetBodyOverFlowHiddenString });

            var pageHeightString = await webView.InvokeScriptAsync("eval", new[] { "document.body.scrollHeight.toString()" });

            if (int.TryParse(pageHeightString, out int pageHeight))
            {
                webView.Height = pageHeight;
            }
        }

        public static async Task<double> SyncWebViewSizeAsync(WebView webView)
        {
            var pageHeightString = await webView.InvokeScriptAsync("eval", new[] { "document.body.scrollHeight.toString()" });

            if (int.TryParse(pageHeightString, out int pageHeight))
            {
                return webView.Height = pageHeight;
            }
            else return 0d;
        }
    }
}
