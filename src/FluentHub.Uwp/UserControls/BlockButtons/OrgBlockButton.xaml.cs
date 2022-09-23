using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.BlockButtons;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.BlockButtons
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

        public OrgBlockButton() => InitializeComponent();

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
