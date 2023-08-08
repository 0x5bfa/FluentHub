using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.BlockButtons
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

        public PullBlockButton()
            => InitializeComponent();

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var navService = Ioc.Default.GetRequiredService<INavigationService>();
            navService.Navigate<Views.Repositories.PullRequests.ConversationPage>(
                new FrameNavigationParameter()
                {
                    PrimaryText = ViewModel.PullItem.Repository.Owner.Login,
                    SecondaryText = ViewModel.PullItem.Repository.Name,
                    Number = ViewModel.PullItem.Number,
                });
        }
    }
}
