using FluentHub.Services;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.AppSettings
{
    public sealed partial class CodePreviewPage : Page
    {
        public CodePreviewPage()
        {
            this.InitializeComponent();
        }

        private readonly INavigationService navigationService;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Code Preview";
            currentItem.Description = "Code Preview settings";
            currentItem.Url = "fluenthub://settings?page=codepreview";
            currentItem.DisplayUrl = $"Settings/Repositories";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uE713"
            };
        }
    }
}
