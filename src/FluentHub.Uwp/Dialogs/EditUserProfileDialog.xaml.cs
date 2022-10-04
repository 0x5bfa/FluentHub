using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Dialogs
{
    public sealed partial class UserProfileEditor : ContentDialog
    {
        public UserProfileEditor() => InitializeComponent();

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
