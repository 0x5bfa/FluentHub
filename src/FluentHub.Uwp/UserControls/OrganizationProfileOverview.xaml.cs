using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls
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
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault();

            OrgNavView.SelectedItem
                = OrgNavView
                .MenuItems
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
                ?? defaultItem;
        }

        private void OnOrgNavViewItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItemContainer.Tag.ToString().ToLower())
            {
                case "overview":
                    navService.Navigate<Views.Organizations.OverviewPage>(
                    new Models.FrameNavigationArgs()
                    {
                        Login = ViewModel.Organization.Login,
                    });
                    break;
                case "repositories":
                    navService.Navigate<Views.Organizations.RepositoriesPage>(
                    new Models.FrameNavigationArgs()
                    {
                        Login = ViewModel.Organization.Login,
                    });
                    break;
            }
        }
    }
}
