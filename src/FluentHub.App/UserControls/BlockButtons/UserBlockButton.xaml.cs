using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.BlockButtons
{
    public sealed partial class UserBlockButton : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(User),
                typeof(UserBlockButtonViewModel),
                typeof(UserBlockButton),
                new PropertyMetadata(null));

        public UserBlockButtonViewModel ViewModel
        {
            get => (UserBlockButtonViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                this.DataContext = ViewModel;
            }
        }
        #endregion

        public UserBlockButton()
            => InitializeComponent();

        private void UserBlockButtonButton_Click(object sender, RoutedEventArgs e)
        {
            var service = App.Current.Services.GetRequiredService<INavigationService>();

            if (ViewModel.User.Id.ToString().StartsWith("O_"))
            {
                service.Navigate<Views.Organizations.OverviewPage>(ViewModel.User.Login);
            }
            else
            {
                service.Navigate<Views.Users.OverviewPage>(
                    new Models.FrameNavigationParameter()
                    {
                        Login = ViewModel.User.Login,
                    });
            }
        }
    }
}
