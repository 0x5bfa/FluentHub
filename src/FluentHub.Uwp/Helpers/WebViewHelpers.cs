using Microsoft.UI.Xaml.Controls;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Helpers
{
    public class WebViewHelpers
    {
        private static readonly string SetBodyOverFlowHiddenString = @"function SetBodyOverFlowHidden() { document.body.style.overflow = 'hidden'; } SetBodyOverFlowHidden();";

        public static async Task DisableWebViewVerticalScrollingAsync(WebView webView)
        {
            _ = await webView.InvokeScriptAsync("eval", new[] { @"function SetBodyOverFlowHidden() { document.body.style.overflow = 'hidden'; } SetBodyOverFlowHidden();" });

            int width;
            int height;

            // get the total width and height
            var widthString = await webView.InvokeScriptAsync("eval", new[] { "document.body.scrollWidth.toString()" });
            var heightString = await webView.InvokeScriptAsync("eval", new[] { "document.body.scrollHeight.toString()" });

            if (!int.TryParse(widthString, out width))
            {
                throw new Exception("Unable to get page width");
            }
            if (!int.TryParse(heightString, out height))
            {
                throw new Exception("Unable to get page height");
            }

            // resize the webview to the content
            //webView.Width = width;
            webView.Height = height;
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
