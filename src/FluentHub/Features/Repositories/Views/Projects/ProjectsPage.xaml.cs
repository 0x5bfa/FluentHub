using FluentHub.Services;
using FluentHub.Features.Repositories.ViewModels.Projects;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Features.Repositories.Views.Projects
{
	public sealed partial class ProjectsPage : NavigableView
	{
		public ProjectsViewModel ViewModel { get; }

		public ProjectsPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Projects)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<ProjectsViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryProjectsPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadRepositoryProjectsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadRepositoryProjectsFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
