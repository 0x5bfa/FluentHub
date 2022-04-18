using FluentHub.Services;
using FluentHub.ViewModels.Repositories.Issues;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Repositories.Issues
{
    public sealed partial class IssuesPage : Page
    {
        public IssuesPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<IssuesViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public IssuesViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var nameWithOwner = e.Parameter as string;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Issues";
            currentItem.Description = "Viewer's issues";
            currentItem.Url = $"https://github.com/issues";
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Issues.png"))
            };

            var command = ViewModel.RefreshIssuesPageCommand;
            if (command.CanExecute(nameWithOwner))
                command.Execute(nameWithOwner);
        }
    }
}
