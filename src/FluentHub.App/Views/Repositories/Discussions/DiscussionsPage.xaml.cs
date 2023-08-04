using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Discussions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Discussions
{
    public sealed partial class DiscussionsPage : Page
    {
        public DiscussionsPage()
        {
            InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<DiscussionsViewModel>();
            _navigation = Ioc.Default.GetRequiredService<INavigationService>();
        }

        public DiscussionsViewModel ViewModel { get; }
        private readonly INavigationService _navigation;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationParameter;
            ViewModel.Login = param.UserLogin;
            ViewModel.Name = param.RepositoryName;

            var command = ViewModel.LoadRepositoryDiscussionsPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
