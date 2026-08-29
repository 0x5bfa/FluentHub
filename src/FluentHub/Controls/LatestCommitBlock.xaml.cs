using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Controls;
using FluentHub.ViewModels.Repositories;
using FluentHub.Views.Repositories.Commits;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls
{
	public sealed partial class LatestCommitBlock : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ContextViewModelProperty =
			DependencyProperty.Register(
			  nameof(ContextViewModel),
			  typeof(RepoContextViewModel),
			  typeof(LatestCommitBlock),
			  new PropertyMetadata(null)
			);

		public RepoContextViewModel ContextViewModel
		{
			get => (RepoContextViewModel)GetValue(ContextViewModelProperty);
			set => SetValue(ContextViewModelProperty, value);
		}
		#endregion

		public LatestCommitBlock()
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<LatestCommitBlockViewModel>();
			navigationService = Ioc.Default.GetRequiredService<INavigationService>();
		}

		public LatestCommitBlockViewModel ViewModel { get; }
		private readonly INavigationService navigationService;

		private void OnLatestCommitBlockLoaded(object sender, RoutedEventArgs e)
		{
			ViewModel.ContextViewModel = ContextViewModel;

			var command = ViewModel.LoadLatestCommitBlockCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnToggleDisplayCommitMessageButtonClick(object sender, RoutedEventArgs e)
		{
			SubCommitMessagesGrid.Visibility =
				SubCommitMessagesGrid.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
		}

		private async void OnViewAllCommitsButtonClick(object sender, RoutedEventArgs e)
		{
			var context = ViewModel.ContextViewModel;
			var repository = new RepositorySlug(context.Repository.Owner.Login, context.Repository.Name);
			await navigationService.NavigateAsync(
				new RepositoryCommitsRoute(repository, context.BranchName, context.Path));
		}
	}
}
