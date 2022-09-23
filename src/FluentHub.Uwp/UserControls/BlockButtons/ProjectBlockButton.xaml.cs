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
    public sealed partial class ProjectBlockButton : UserControl
    {
        #region dprops
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(Project),
                  typeof(ProjectBlockButtonViewModel),
                  typeof(ProjectBlockButton),
                  new PropertyMetadata(null)
                );

        public ProjectBlockButtonViewModel ViewModel
        {
            get => (ProjectBlockButtonViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = ViewModel;
            }
        }
        #endregion

        public ProjectBlockButton() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var navService = App.Current.Services.GetRequiredService<INavigationService>();
            //navService.Navigate<ProjectPage>(
            //    new FrameNavigationArgs()
            //    {
            //        Login = ViewModel.Item.Repository.Owner.Login,
            //        Name = ViewModel.Item.Repository.Name,
            //        Number = ViewModel.Item.Number,
            //    });
        }
    }
}
