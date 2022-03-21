using FluentHub.Octokit.Models;
using FluentHub.Services;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Views.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.UserControls.ButtonBlocks
{
    public sealed partial class PullButtonBlock : UserControl
    {
        #region dprops
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(PullRequest),
                  typeof(PullButtonBlockViewModel),
                  typeof(PullButtonBlock),
                  new PropertyMetadata(null)
                );

        public PullButtonBlockViewModel ViewModel
        {
            get => (PullButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                this.DataContext = ViewModel;
                ViewModel.SetStateContents();
            }
        }
        #endregion

        public PullButtonBlock()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        private async void IssueBlockButton_Click(object sender, RoutedEventArgs e)
        {
            var repo = await App.Client.Repository.Get(ViewModel.PullItem.Owner, ViewModel.PullItem.Name);

            string param = repo.Id + "/" + ViewModel.PullItem.Number;
            navigationService.Navigate<IssuePage>(param);
            //App.MainViewModel.RepoMainFrame.Navigate(typeof(IssuePage), param);
        }
    }
}
