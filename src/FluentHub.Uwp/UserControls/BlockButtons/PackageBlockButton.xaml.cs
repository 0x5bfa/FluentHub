using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.UserControls.BlockButtons;
using FluentHub.Uwp.Views.Repositories.Projects;
using Microsoft.Extensions.DependencyInjection;
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

namespace FluentHub.Uwp.UserControls.BlockButtons
{
    public sealed partial class PackageBlockButton : UserControl
    {
        #region dprops
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(ViewModel),
                  typeof(PackageBlockButtonViewModel),
                  typeof(PackageBlockButton),
                  new PropertyMetadata(null)
                );

        public PackageBlockButtonViewModel ViewModel
        {
            get => (PackageBlockButtonViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = ViewModel;
            }
        }
        #endregion

        public PackageBlockButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
