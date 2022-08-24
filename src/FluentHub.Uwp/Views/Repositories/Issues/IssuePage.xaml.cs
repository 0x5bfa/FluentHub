using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories.Issues;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories.Issues
{
    public sealed partial class IssuePage : Page
    {
        public IssuePage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<IssueViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public IssueViewModel ViewModel { get; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationArgs;
            ViewModel.Number = param.Number;

            await ViewModel.LoadRepositoryAsync(param.Login, param.Name);

            var command = ViewModel.RefreshIssuePageCommand;
            if (command.CanExecute(null))
                await command.ExecuteAsync(null);

            SetCurrentTabItem();
        }

        private void SetCurrentTabItem()
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{ViewModel.IssueItem.Title} · #{ViewModel.IssueItem.Number}";
            currentItem.Description = currentItem.Header;
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Issues.png"))
            };
        }

        private void OnDisplayDetailsTogglingButtonClick(object sender, RoutedEventArgs e)
        {
            if (DetailsGridColumnDefinition.Width.IsStar)
            {
                DetailsGridColumnDefinition.Width = new GridLength(0, GridUnitType.Pixel);
                DetailsGridColumnDefinition.MinWidth = 0;
            }
            else
            {
                DetailsGridColumnDefinition.Width = new GridLength(1, GridUnitType.Star);
                DetailsGridColumnDefinition.MinWidth = 214;
            }
        }

        //private async void OnCommentWebViewNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        //{
        //    await Helpers.WebViewHelpers.DisableWebViewVerticalScrollingAsync(sender);
        //}

        //private async void OnWebViewSizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    if (sender as WebView != null && WebViewIsNavigatedSuccessfully)
        //    {
        //        await Helpers.WebViewHelpers.DisableWebViewVerticalScrollingAsync(sender as WebView);
        //    }
        //}

        //private async void OnCommentWebViewLoaded(object sender, RoutedEventArgs e)
        //{
        //    WebView wv = sender as WebView;
        //    IssueComment ic = wv.Tag as IssueComment;

        //    if (!string.IsNullOrEmpty(ViewModel.IssueCommentDataItems.FirstOrDefault(x => x == ic.Id.ToString())))
        //    {
        //        return;
        //    }

        //    Services.MarkdownApiHandler markdown = new();
        //    var html = await markdown.GetHtmlAsync(ic?.BodyHTML, ic?.Url, Helpers.ThemeHelper.ActualTheme.ToString().ToLower());
        //    wv.NavigateToString(html);

        //    ViewModel.IssueCommentDataItems.Add(ic.Id.ToString());
        //}
    }
}
