using FluentHub.Services;
using FluentHub.ViewModels.Home;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Home
{
    public sealed partial class ActivitiesPage : Page
    {
        public ActivitiesPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ActivitiesViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        public ActivitiesViewModel ViewModel { get; }
        private readonly INavigationService navigationService;        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = e.Parameter;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Activities";
            currentItem.Description = "Viewer's activities";
            currentItem.Url = "https://github.com";
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.FontIconSource
            {
                Glyph = "\uECAD"
            };
                        
            var command = ViewModel.RefreshActivitiesCommand;
            if (command.CanExecute(DataContext))
                command.Execute(DataContext);
        }
    }
}