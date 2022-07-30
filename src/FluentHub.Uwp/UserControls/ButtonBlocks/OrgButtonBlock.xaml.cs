using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.ButtonBlocks
{
    public sealed partial class OrgButtonBlock : UserControl
    {
        #region dprops
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(Organization),
                  typeof(IssueButtonBlockViewModel),
                  typeof(OrgButtonBlock),
                  new PropertyMetadata(null)
                );

        public OrgButtonBlockViewModel ViewModel
        {
            get => (OrgButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
            }
        }
        #endregion

        public OrgButtonBlock() => InitializeComponent();

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
