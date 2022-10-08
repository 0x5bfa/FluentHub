using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Searches;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Uwp.Views.Searches
{
    public sealed partial class IssuesPage : Page
    {
        public IssuesPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<IssuesViewModel>();
        }

        public IssuesViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as Models.FrameNavigationArgs;
            ViewModel.SearchTerm = param.Parameters.ElementAtOrDefault(0) as string;

            var command = ViewModel.LoadSearchIssuesPageCommand;
            if (command.CanExecute(null))
                command.ExecuteAsync(null);
        }
    }
}
