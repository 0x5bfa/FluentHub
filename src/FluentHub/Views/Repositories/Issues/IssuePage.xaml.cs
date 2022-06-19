using FluentHub.Services;
using FluentHub.ViewModels.Repositories.Issues;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Repositories.Issues
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
            var url = e.Parameter as string;
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split("/").ToList();
            pathSegments.RemoveAt(0);

            var command = ViewModel.RefreshIssuePageCommand;
            if (command.CanExecute(url))
                await command.ExecuteAsync(url);

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{ViewModel.IssueItem.Title} · #{ViewModel.IssueItem.Number}";
            currentItem.Description = currentItem.Header;
            currentItem.Url = url;

            currentItem.DisplayUrl = $"{pathSegments[0]} / {pathSegments[1]} / {pathSegments[3]}";

            currentItem.DisplayUrl = $"{ViewModel.IssueItem.OwnerLogin} / {ViewModel.IssueItem.Name} / {ViewModel.IssueItem.Number}";

            currentItem.Icon = new Microsoft.UI.Xaml.Controls.ImageIconSource
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
    }
}
