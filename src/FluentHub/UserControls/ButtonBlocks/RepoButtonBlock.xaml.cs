using FluentHub.OctokitEx.Models;
using FluentHub.Services;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.UserControls.ButtonBlocks
{
    public sealed partial class RepoButtonBlock : UserControl
    {
        #region properties
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(RepositoryBlockItem), typeof(RepoButtonBlockViewModel), typeof(RepoButtonBlock), new PropertyMetadata(null));

        public RepoButtonBlockViewModel ViewModel
        {
            get => (RepoButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                this.DataContext = ViewModel;
                ViewModel?.GetColorBrush();
                UpdateVisibility();
            }
        }
        #endregion

        public RepoButtonBlock() => InitializeComponent();

        public void UpdateVisibility()
        {
            if (ViewModel?.Item?.PrimaryLangName == null)
            {
                LanguageBlock.Visibility = Visibility.Collapsed;
            }

            // does not work property
            if (LicenseBlock != null && string.IsNullOrEmpty(ViewModel?.Item.LicenseName))
            {
                LicenseBlock.Visibility = Visibility.Collapsed;
            }
        }

        private async void RepoBlockButton_Click(object sender, RoutedEventArgs e)
        {
            var repo = await App.Client.Repository.Get(ViewModel.Item.Owner, ViewModel.Item.Name);
            var service = App.Current.Services.GetRequiredService<INavigationService>();
            service.Navigate<Views.Repositories.OverviewPage>(repo.Id.ToString());
        }
    }
}