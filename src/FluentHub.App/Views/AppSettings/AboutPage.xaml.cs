using FluentHub.App.Services;
using FluentHub.App.ViewModels.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.AppSettings
{
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<AboutViewModel>();
            _navigation = Ioc.Default.GetRequiredService<INavigationService>();
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
