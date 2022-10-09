using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Home;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Uwp.Views.Home
{
    public sealed partial class ActivitiesPage : Page
    {
        public ActivitiesPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<FeedsViewModel>();
            _navigation = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService _navigation;
        public FeedsViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var command = ViewModel.LoadUserFeedsPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void OnHomeRepositoriesListItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as Repository;

            var parameter = new FrameNavigationArgs()
            {
                Login = item.Owner.Login,
                Name = item.Name,
            };

            if (App.AppSettings.UseDetailsView)
                _navigation.Navigate<Views.Repositories.Code.Layouts.DetailsLayoutView>(parameter);
            else
                _navigation.Navigate<Views.Repositories.Code.Layouts.TreeLayoutView>(parameter);
        }
    }
}
