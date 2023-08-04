using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories;
using FluentHub.App.ViewModels.Repositories.Commits;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.App.Data.Parameters;

namespace FluentHub.App.Views.Repositories.Commits
{
    public sealed partial class CommitPage : Page
    {
        public CommitPage()
        {
            InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<CommitViewModel>();
            _navigation = Ioc.Default.GetRequiredService<INavigationService>();
        }

        public CommitViewModel ViewModel { get; }
        private readonly INavigationService _navigation;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationParameter;
            ViewModel.Login = param.UserLogin;
            ViewModel.Name = param.RepositoryName;
            ViewModel.CommitItem = param.Parameters.ElementAt(0) as Commit;

            var command = ViewModel.LoadRepositoryCommitPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
