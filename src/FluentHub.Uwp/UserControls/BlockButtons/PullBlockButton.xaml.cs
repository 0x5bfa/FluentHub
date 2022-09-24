using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.BlockButtons;
using FluentHub.Uwp.Views.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.BlockButtons
{
    public sealed partial class PullBlockButton : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                  nameof(PullRequest),
                  typeof(PullBlockButtonViewModel),
                  typeof(PullBlockButton),
                  new PropertyMetadata(null)
                );

        public PullBlockButtonViewModel ViewModel
        {
            get => (PullBlockButtonViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                ViewModel?.LoadContents();
            }
        }
        #endregion

        public PullBlockButton() => InitializeComponent();

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var navService = App.Current.Services.GetRequiredService<INavigationService>();
            navService.Navigate<Views.Repositories.PullRequests.ConversationPage>(
                new FrameNavigationArgs()
                {
                    Login = ViewModel.PullItem.Repository.Owner.Login,
                    Name = ViewModel.PullItem.Repository.Name,
                    Number = ViewModel.PullItem.Number,
                });
        }
    }
}
