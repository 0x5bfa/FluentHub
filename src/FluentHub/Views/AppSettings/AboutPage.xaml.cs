using FluentHub.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.AppSettings
{
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"About", "About FluentHub", "fluenthub://settings/about", "\uE713");
            var currentItem = App.Current.Services.GetService<ITabItemView>();
            currentItem.Header = "About";
            currentItem.Description = "About FluentHub";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uE713"
            };
        }
    }
}