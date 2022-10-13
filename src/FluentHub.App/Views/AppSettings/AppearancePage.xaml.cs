using FluentHub.App.Services;
using FluentHub.App.ViewModels.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.AppSettings
{
    public sealed partial class AppearancePage : Page
    {
        public AppearancePage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<AppearanceViewModel>();
            _navigation = provider.GetRequiredService<INavigationService>();
        }

        public AppearanceViewModel ViewModel { get; }
        private readonly INavigationService _navigation;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var command = ViewModel.LoadUserCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
