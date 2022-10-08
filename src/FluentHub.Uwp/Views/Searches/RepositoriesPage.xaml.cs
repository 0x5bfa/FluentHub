using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Searches;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Uwp.Views.Searches
{
    public sealed partial class RepositoriesPage : Page
    {
        public RepositoriesPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<RepositoriesViewModel>();
        }

        public RepositoriesViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as Models.FrameNavigationArgs;
            ViewModel.SearchTerm = param.Parameters.ElementAtOrDefault(0) as string;

            var command = ViewModel.LoadSearchRepositoriesPageCommand;
            if (command.CanExecute(null))
                command.ExecuteAsync(null);
        }
    }
}
