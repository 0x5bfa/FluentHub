using FluentHub.Uwp.ViewModels.UserControls;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Uwp.UserControls
{
    public sealed partial class UserContributionGraph : UserControl
    {
        #region propdp
        public static readonly DependencyProperty LoginProperty =
            DependencyProperty.Register(
                nameof(Login),
                typeof(string),
                typeof(UserContributionGraph),
                new PropertyMetadata(null));

        public string Login
        {
            get => (string)GetValue(LoginProperty);
            set
            {
                SetValue(LoginProperty, value);

                ViewModel.Login = value;
                _ = ViewModel.GetContributionCalendarAsync();
            }
        }
        #endregion

        public UserContributionGraphViewModel ViewModel { get; }

        public UserContributionGraph()
        {
            InitializeComponent();
            ViewModel = new();
        }
    }
}
