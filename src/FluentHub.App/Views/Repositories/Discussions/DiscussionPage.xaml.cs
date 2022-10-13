using FluentHub.App.Models;
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

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<DiscussionViewModel>();
            _navigation = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService _navigation;
        public DiscussionViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationArgs;
            ViewModel.Login = param.Login;
            ViewModel.Name = param.Name;
            ViewModel.Number = param.Number;

            var command = ViewModel.LoadRepositoryDiscussionPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
