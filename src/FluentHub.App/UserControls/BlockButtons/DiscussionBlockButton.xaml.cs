using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.BlockButtons
{
    public sealed partial class DiscussionBlockButton : UserControl
    {
        #region dprops
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(Discussion),
                  typeof(DiscussionBlockButtonViewModel),
                  typeof(DiscussionBlockButton),
                  new PropertyMetadata(null)
                );

        public DiscussionBlockButtonViewModel ViewModel
        {
            get => (DiscussionBlockButtonViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = ViewModel;
            }
        }
        #endregion

        public DiscussionBlockButton()
            => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var navService = App.Current.Services.GetRequiredService<INavigationService>();
            navService.Navigate<Views.Repositories.Discussions.DiscussionsPage>(
                new FrameNavigationParameter()
                {
                    Login = ViewModel.Item.Repository.Owner.Login,
                    Name = ViewModel.Item.Repository.Name,
                    Number = ViewModel.Item.Number,
                });
        }
    }
}
