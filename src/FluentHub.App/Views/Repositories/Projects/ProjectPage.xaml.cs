using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Projects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Projects
{
    public sealed partial class ProjectPage : Page
    {
        public ProjectPage()
        {
            InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<ProjectViewModel>();
            _navigation = Ioc.Default.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService _navigation;
        public ProjectViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationParameter;
            ViewModel.Login = param.PrimaryText;
            ViewModel.Name = param.SecondaryText;
            ViewModel.Number = param.Number;

            var command = ViewModel.LoadRepositoryProjectPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
