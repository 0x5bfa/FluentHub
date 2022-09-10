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
            _navigation = provider.GetRequiredService<INavigationService>();
        }

        public IssueViewModel ViewModel { get; }
        private readonly INavigationService _navigation;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationArgs;
            ViewModel.Login = param.Login;
            ViewModel.Name = param.Name;
            ViewModel.Number = param.Number;

            var command = ViewModel.LoadRepositoryIssuePageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
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
