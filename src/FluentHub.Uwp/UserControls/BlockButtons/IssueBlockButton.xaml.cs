using FluentHub.Octokit.Models;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.UserControls.BlockButtons;
using FluentHub.Uwp.Views.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.BlockButtons
{
    public sealed partial class IssueBlockButton : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                  nameof(IssueBlockButtonViewModel),
                  typeof(IssueBlockButtonViewModel),
                  typeof(IssueBlockButton),
                  new PropertyMetadata(null)
                );

        public IssueBlockButtonViewModel ViewModel
        {
            get => (IssueBlockButtonViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                ViewModel?.LoadContents();
            }
        }
        #endregion
       
        public IssueBlockButton() => InitializeComponent();

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var navService = App.Current.Services.GetRequiredService<INavigationService>();
            navService.Navigate<Views.Repositories.Issues.IssuePage>(
                new FrameNavigationArgs()
                {
                    Login = ViewModel.IssueItem.Repository.Owner.Login,
                    Name = ViewModel.IssueItem.Repository.Name,
                    Number = ViewModel.IssueItem.Number,
                });
        }
    }
}
