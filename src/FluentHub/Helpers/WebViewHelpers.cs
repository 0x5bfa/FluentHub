using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Helpers
{
    public class WebViewHelpers
    {
        private static readonly string SetBodyOverFlowHiddenString = @"function SetBodyOverFlowHidden() { document.body.style.overflow = 'hidden'; } SetBodyOverFlowHidden();";

        public static void DisableWebViewVerticalScrolling(ref WebView webView)
        {
            DisableWebViewVerticalScrollingAsync(webView);
        }

        public static async void DisableWebViewVerticalScrollingAsync(WebView webView)
        {
            _ = await webView.InvokeScriptAsync("eval", new string[] { SetBodyOverFlowHiddenString });

            var pageHeightString = await webView.InvokeScriptAsync("eval", new[] { "document.body.scrollHeight.toString()" });

            if (int.TryParse(pageHeightString, out int pageHeight))
            {
                webView.Height = pageHeight;
            }
        }
    }
}
