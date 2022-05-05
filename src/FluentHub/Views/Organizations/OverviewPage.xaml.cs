using FluentHub.Services;
using FluentHub.ViewModels.Organizations;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Organizations
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
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uEA27",
                FontFamily = new Windows.UI.Xaml.Media.FontFamily("/Assets/Glyphs/Octions.ttf#octions")
            };
            #endregion

            var command = ViewModel.LoadOrganizationOverviewAsyncCommand;
            if (command.CanExecute(DataContext))
                command.Execute(DataContext);

            // Avoid duplicates
            OrgPageFrame.Navigate(typeof(RepositoriesPage), org);
        }
    }
}
