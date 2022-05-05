using FluentHub.Octokit.Models;
using FluentHub.Services;
using FluentHub.ViewModels;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Views.Repositories.Issues;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.UserControls.ButtonBlocks
{
    public sealed partial class IssueButtonBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                  nameof(Issue),
                  typeof(IssueButtonBlockViewModel),
                  typeof(IssueButtonBlock),
                  new PropertyMetadata(null)
                );

        public IssueButtonBlockViewModel ViewModel
        {
            get => (IssueButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = ViewModel;
                ViewModel?.LoadContents();
            }
        }
        #endregion
       
        public IssueButtonBlock()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        private void OnIssueBlockButtonClick(object sender, RoutedEventArgs e)
        {
            MainPageViewModel.RepositoryContentFrame.Navigate(typeof(IssuePage), ViewModel.IssueItem);
        }

        private void OnIssueButtonBlockLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
