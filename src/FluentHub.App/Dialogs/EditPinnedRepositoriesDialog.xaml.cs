using FluentHub.App.Services;
using FluentHub.App.ViewModels.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.Dialogs
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

        private async void OnEditPinnedRepositoriesDialogLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadPinnableAndPinnedRepositories();
        }
    }
}
