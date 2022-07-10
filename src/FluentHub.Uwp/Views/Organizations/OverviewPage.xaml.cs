using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Organizations;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Organizations
{
    public sealed partial class OverviewPage : Page
    {
        public OverviewPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<OverviewViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public OverviewViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string org = e.Parameter as string;
            DataContext = org;

            #region tabitem
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{org}";
            currentItem.Description = $"{org}";
            currentItem.Url = $"https://github.com/{org}";
            currentItem.DisplayUrl = $"{org}";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Organizations.png"))
            };
            #endregion

            var command = ViewModel.LoadOrganizationOverviewAsyncCommand;
            if (command.CanExecute(DataContext))
                command.Execute(DataContext);
        }
    }
}
