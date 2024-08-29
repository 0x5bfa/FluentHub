using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.Extensions
{
	public static class WebViewExtensions
	{
		public static async Task HandleResize(this WebView2 webView2)
		{
			try
			{
				// heightString will be like '"800"'
				var heightString = await webView2.ExecuteScriptAsync("document.getElementById('container').scrollHeight.toString()");

				heightString = heightString.Trim('"');

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
