using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Uwp.Views.AppSettings
{
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<AboutViewModel>();
            _navigation = provider.GetRequiredService<INavigationService>();
        }

        public AboutViewModel ViewModel { get; }
        private readonly INavigationService _navigation;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var command = ViewModel.LoadUserCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
