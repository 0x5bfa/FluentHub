using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Searches;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Searches
{
    public sealed partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<UsersViewModel>();
        }

        public UsersViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as Models.FrameNavigationArgs;
            ViewModel.SearchTerm = param.Parameters.ElementAtOrDefault(0) as string;

            var command = ViewModel.LoadSearchUsersPageCommand;
            if (command.CanExecute(null))
                command.ExecuteAsync(null);
        }
    }
}
