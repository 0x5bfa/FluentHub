using FluentHub.App.ViewModels.Repositories;
using FluentHub.App.ViewModels.Repositories.Commits;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Commits
{
	public sealed partial class CommitsPage : LocatablePage
	{
		public CommitsViewModel ViewModel { get; }
		private readonly INavigationService _navigation;

		public CommitsPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<CommitsViewModel>();
			_navigation = Ioc.Default.GetRequiredService<INavigationService>();
			_pageLoadCommand = ViewModel.LoadRepositoryCommitsPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var param = e.Parameter as FrameNavigationParameter;
			ViewModel.ContextViewModel = param.Parameters as RepoContextViewModel;

			var command = ViewModel.LoadRepositoryCommitsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadRepositoryCommitsFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
