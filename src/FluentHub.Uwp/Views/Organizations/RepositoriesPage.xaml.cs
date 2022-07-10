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
    public sealed partial class RepositoriesPage : Page
    {
        public RepositoriesPage()
        {
            InitializeComponent();
            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<RepositoriesViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public RepositoriesViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = e.Parameter;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Repositories";
            currentItem.Description = $"{DataContext}'s repositories";
            currentItem.Url = $"https://github.com/orgs/{DataContext}/repositories";
            currentItem.DisplayUrl = $"{DataContext} / Repositories";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };

            var command = ViewModel.RefreshRepositoriesCommand;
            if (command.CanExecute(DataContext))
                command.Execute(DataContext);
        }
    }
}
