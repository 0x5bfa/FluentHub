using FluentHub.App.Services;
using FluentHub.App.ViewModels.SignIn;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.SignIn
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
