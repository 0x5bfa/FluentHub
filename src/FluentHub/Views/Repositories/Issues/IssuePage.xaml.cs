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
            currentItem.Header = $"{ViewModel.IssueItem.Title} · Issue #{ViewModel.IssueItem.Number} · {ViewModel.IssueItem.OwnerLogin}/{ViewModel.IssueItem.Name}";
            currentItem.Description = currentItem.Header;
            currentItem.Url = url;
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Issues.png"))
            };
        }
    }
}
