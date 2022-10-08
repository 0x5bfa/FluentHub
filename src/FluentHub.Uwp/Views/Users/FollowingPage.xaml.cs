using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Uwp.Views.Users
{
    public sealed partial class FollowingPage : Page
    {
        public FollowingPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<FollowingViewModel>();
        }

        public FollowingViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as Models.FrameNavigationArgs;
            ViewModel.Login = param.Login;

            if (param.Parameters.ElementAtOrDefault(0) as string is "AsViewer")
            {
                ViewModel.DisplayTitle = true;
            }

            var command = ViewModel.LoadUserFollowingPageCommand;
            if (command.CanExecute(null))
                command.ExecuteAsync(null);
        }
    }
}
