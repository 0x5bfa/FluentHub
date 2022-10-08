using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories.Projects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Uwp.Views.Repositories.Projects
{
    public sealed partial class ProjectPage : Page
    {
        public ProjectPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ProjectViewModel>();
            _navigation = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService _navigation;
        public ProjectViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationArgs;
            ViewModel.Login = param.Login;
            ViewModel.Name = param.Name;
            ViewModel.Number = param.Number;

            var command = ViewModel.LoadRepositoryProjectPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
