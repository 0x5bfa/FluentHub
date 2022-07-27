using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Services.Navigation;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.Repositories.Codes.Layouts;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories.Codes.Layouts
{
    public sealed partial class DetailsLayoutView : Page
    {
        public DetailsLayoutView()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<DetailsLayoutViewModel>();
            navService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        public DetailsLayoutViewModel ViewModel { get; }
        private readonly INavigationService navService;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationArgs;

            await ViewModel.LoadRepositoryAsync(param.Login, param.Name);

            ViewModel.InitializeRepositoryContext(param.Login, param.Name, param.Parameters.ElementAtOrDefault(0) as string);

            await ViewModel.LoadRepositoryContentsAsync();
        }

        private void OnDirListViewDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var item = DirListView.SelectedItem as DetailsLayoutListViewModel;
            var objType = item?.Type;

            string path = ViewModel.ContextViewModel.Path;

            if (string.IsNullOrEmpty(path) is false)
            {
                path += "/";
            }

            path += item.Name;
            List<object> param = new();
            param.Add(objType + "/" + ViewModel.ContextViewModel.BranchName + "/" + path);

            navService.Navigate<DetailsLayoutView>(
                new FrameNavigationArgs()
                {
                    Login = ViewModel.Repository.Owner.Login,
                    Name = ViewModel.Repository.Name,
                    Parameters = param,
                });
        }

        private void OnLatestReleaseClick(object sender, RoutedEventArgs e)
        {
            navService.Navigate<ReleasesPage>(ViewModel.ContextViewModel);
        }
    }
}
