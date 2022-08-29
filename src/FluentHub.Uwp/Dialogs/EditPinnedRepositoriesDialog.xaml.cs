using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Dialogs
{
    public sealed partial class EditPinnedRepositoriesDialog : ContentDialog
    {
        public EditPinnedRepositoriesDialog(string login = null)
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<EditPinnedRepositoriesDialogViewModel>();

            ViewModel.Login = login;
        }

        private readonly INavigationService navigationService;
        public EditPinnedRepositoriesDialogViewModel ViewModel { get; }

        private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private async void OnEditPinnedRepositoriesDialogLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadPinnableAndPinnedRepositories();
        }
    }
}
