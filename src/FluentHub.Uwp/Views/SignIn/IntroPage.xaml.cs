using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.SignIn;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Uwp.Views.SignIn
{
    public sealed partial class IntroPage : Page
    {
        public IntroPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<IntroViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        public IntroViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
            => Window.Current.SetTitleBar(AppTitleBar);
    }
}
