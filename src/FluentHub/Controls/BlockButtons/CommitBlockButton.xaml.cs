using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Controls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace FluentHub.Controls.BlockButtons
{
	public sealed partial class CommitBlockButton : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(CommitBlockButtonViewModel),
				typeof(CommitBlockButtonViewModel),
				typeof(CommitBlockButton),
				new PropertyMetadata(null));

		public CommitBlockButtonViewModel ViewModel
		{
			get => (CommitBlockButtonViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}
		#endregion

		public CommitBlockButton()
			=> InitializeComponent();

		private async void CommitItemButton_Click(object sender, RoutedEventArgs e)
		{
			var repository = new RepositorySlug(
				ViewModel.CommitItem.Repository.Owner.Login,
				ViewModel.CommitItem.Repository.Name);
			AppRoute route;

			if (ViewModel.PullRequest == null)
			{
				route = new RepositoryCommitRoute(repository, ViewModel.CommitItem.Oid);
			}
			else
			{
				route = new RepositoryPullRequestCommitRoute(
					repository,
					ViewModel.PullRequest.Number,
					ViewModel.CommitItem.Oid);
			}

			var navService = Ioc.Default.GetRequiredService<INavigationService>();
			await navService.NavigateAsync(route);
		}
	}
}
