using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.BlockButtons
{
    public sealed partial class OrgBlockButton : UserControl
    {
        #region dprops
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(Organization),
                  typeof(IssueBlockButtonViewModel),
                  typeof(OrgBlockButton),
                  new PropertyMetadata(null)
                );

        public OrgBlockButtonViewModel ViewModel
        {
            get => (OrgBlockButtonViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
            }
        }
        #endregion

        public OrgBlockButton()
            => InitializeComponent();

        private void OrganizationOverviewButton_Click(object sender, RoutedEventArgs e)
        {
            var service = App.Current.Services.GetRequiredService<INavigationService>();
            service.Navigate<Views.Organizations.OverviewPage>(
                new Models.FrameNavigationArgs()
                {
                    Login = ViewModel.OrgItem.Login,
                });
        }
    }
}
