using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Home;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Home
{
    public sealed partial class ActivitiesPage : Page
    {
        public ActivitiesPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ActivitiesViewModel>();
            navService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navService;
        public ActivitiesViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetCurrentTabItem();

            var command = ViewModel.RefreshActivitiesCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void SetCurrentTabItem()
        {
            var currentItem = navService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Activities";
            currentItem.Description = "Viewer's activities";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Activities.png"))
            };
        }

        private void OnHomeRepositoriesListItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as Repository;

            navService.Navigate<Repositories.Codes.CodePage>(
                new Models.FrameNavigationArgs()
                {
                    Login = clickedItem.Owner.Login,
                    Name = clickedItem.Name,
                });
        }
    }
}
