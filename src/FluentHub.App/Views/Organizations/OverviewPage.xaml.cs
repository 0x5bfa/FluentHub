using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Organizations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Organizations
{
    public sealed partial class OverviewPage : Page
    {
        public OverviewPage()
        {
            InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<OverviewViewModel>();
        }

        public OverviewViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationParameter;
            ViewModel.Login = param.UserLogin;

            var command = ViewModel.LoadOrganizationOverviewPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
