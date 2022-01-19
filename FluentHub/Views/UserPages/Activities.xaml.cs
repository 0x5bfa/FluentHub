using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace FluentHub.Views.UserPages
{
    public sealed partial class Activities : Page
    {
        public Activities()
        {
            this.InitializeComponent();
        }

        private async void WebViewSample_Loaded(object sender, RoutedEventArgs e)
        {
            Services.OctokitEx.Markdown markdown = new Services.OctokitEx.Markdown();

            var item = await App.Client.Repository.Content.GetReadmeHtml($"{App.SignedInUserName}", $"{App.SignedInUserName}");

            StorageFile template = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/WebView/RenderedMarkdownTemplate.html"));
            StorageFile style = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/WebView/github-markdown-dark.css"));

            string templateText = await Windows.Storage.FileIO.ReadTextAsync(template);
            string styleText = await Windows.Storage.FileIO.ReadTextAsync(style);

            string result = templateText.Replace("{0}", styleText);
            result = result.Replace("{1}", item);

            WebViewSample.NavigateToString(result);
        }

        private void WebViewSample_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if(args.Uri != null)
            {
                args.Cancel = true;
            }
        }
    }
}
