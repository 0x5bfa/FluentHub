using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

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
