using FluentHub.App.ViewModels.Repositories;
using FluentHub.App.ViewModels.UserControls;
using FluentHub.App.Views.Repositories.Commits;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls
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

		private void OnViewAllCommitsButtonClick(object sender, RoutedEventArgs e)
		{
			navigationService.Navigate<CommitsPage>(
				new FrameNavigationParameter()
				{
					PrimaryText = ViewModel.ContextViewModel.Repository.Owner.Login,
					SecondaryText = ViewModel.ContextViewModel.Repository.Name,
					Parameters = ViewModel.ContextViewModel,
				});
		}
	}
}
