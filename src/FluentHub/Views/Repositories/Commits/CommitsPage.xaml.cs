using FluentHub.Services;
using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.Repositories.Commits;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Data.Parameters;

namespace FluentHub.Views.Repositories.Commits
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
			if (e.Parameter is not FrameNavigationParameter { Parameters: RepoContextViewModel context })
				return;

			ViewModel.ContextViewModel = context;

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
