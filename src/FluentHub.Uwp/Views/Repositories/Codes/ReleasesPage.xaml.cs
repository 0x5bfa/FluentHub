using FluentHub.Uwp.Services;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.ViewModels.Repositories.Codes;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories.Codes
{
    public sealed partial class ReleasesPage : Page
    {
        public ReleasesPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ReleasesViewModel>();
            navService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        public ReleasesViewModel ViewModel { get; }
        private readonly INavigationService navService;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationArgs;

            await ViewModel.LoadRepositoryAsync(param.Login, param.Name);

            SetCurrentTabItem();

            var command = ViewModel.LoadReleasesPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void SetCurrentTabItem()
        {
            var currentItem = navService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Releases · {ViewModel.Repository.Owner.Login}/{ViewModel.Repository.Name}";
            currentItem.Description = $"Releases · {ViewModel.Repository.Owner.Login}/{ViewModel.Repository.Name}";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };
        }
    }
}
