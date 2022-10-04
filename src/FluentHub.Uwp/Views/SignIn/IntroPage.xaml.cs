using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.SignIn;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Uwp.Views.SignIn
{
    public sealed partial class IntroPage : Page
    {
        public IntroPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<IntroViewModel>();
        }

        public IntroViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
            => App.Window.SetTitleBar(AppTitleBar);
    }
}
