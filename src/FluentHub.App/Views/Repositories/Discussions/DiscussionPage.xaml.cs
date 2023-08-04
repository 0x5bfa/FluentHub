using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Discussions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Discussions
{
    public sealed partial class DiscussionPage : Page
    {
        public DiscussionPage()
        {
            InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<DiscussionViewModel>();
            _navigation = Ioc.Default.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService _navigation;
        public DiscussionViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationParameter;
            ViewModel.Login = param.UserLogin;
            ViewModel.Name = param.RepositoryName;
            ViewModel.Number = param.Number;

            var command = ViewModel.LoadRepositoryDiscussionPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
