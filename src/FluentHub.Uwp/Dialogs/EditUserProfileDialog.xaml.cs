using FluentHub.Uwp.Services;
using FluentHub.Uwp.Services.Navigation;
using FluentHub.Uwp.ViewModels.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.Dialogs
{
    public sealed partial class UserProfileEditor : ContentDialog
    {
        public User UpdatedUser { get; set; }

        public UserProfileEditor() => InitializeComponent();

        public UserProfileEditor(User currentUser)
        {
            InitializeComponent();
            UpdatedUser = currentUser;
            UserName.Text = currentUser.Name ?? "";
            UserBio.Text = currentUser.Bio ?? "";
            UserCompany.Text = currentUser.Company ?? "";
            UserLink.Text = currentUser.WebsiteUrl ?? "";
            UserLocation.Text = currentUser.Location ?? "";
            UserEmail.Text = currentUser.Email ?? "";
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();

            UpdatedUser.Name = UserName.Text;
            UpdatedUser.Bio = UserBio.Text;
            UpdatedUser.Company = UserCompany.Text;
            UpdatedUser.WebsiteUrl = UserLink.Text;
            UpdatedUser.Location = UserLocation.Text;

            deferral.Complete();
        }
    }
}
