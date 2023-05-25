using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace FluentHub.App.UserControls.Overview
{
    public sealed partial class OrganizationProfileOverview : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(OrganizationProfileOverviewViewModel),
                typeof(OrganizationProfileOverviewViewModel),
                new PropertyMetadata(null));

        public OrganizationProfileOverviewViewModel ViewModel
        {
            get => (OrganizationProfileOverviewViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                if (ViewModel is not null)
                    SelectItemByTag(ViewModel.SelectedTag);
            }
        }
        #endregion

        public OrganizationProfileOverview()
        {
            InitializeComponent();
            navService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navService;

        private void SelectItemByTag(string tag)
        {
            var defaultItem
                = OrgNavView
                .MenuItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault();

            OrgNavView.SelectedItem
                = OrgNavView
                .MenuItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
                ?? defaultItem;
        }

        private void OnOrgNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItemContainer.Tag.ToString().ToLower())
            {
                case "overview":
                    navService.Navigate<Views.Organizations.OverviewPage>(
                    new Models.FrameNavigationParameter()
                    {
                        Login = ViewModel.Organization.Login,
                    });
                    break;
                case "repositories":
                    navService.Navigate<Views.Organizations.RepositoriesPage>(
                    new Models.FrameNavigationParameter()
                    {
                        Login = ViewModel.Organization.Login,
                    });
                    break;
            }
        }

        private void OnVerifiedLabelTapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
            => FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }
}
