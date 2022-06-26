using FluentHub.Services;
using FluentHub.ViewModels.SignIn;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.SignIn
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

        private readonly INavigationService navigationService;
        public IntroViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
            => Window.Current.SetTitleBar(AppTitleBar);
    }
}
