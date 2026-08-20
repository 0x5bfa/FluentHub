using FluentHub.Data.Parameters;
using FluentHub.Services;
using FluentHub.ViewModels.Repositories.Projects;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Views.Repositories.Projects
{
	public sealed partial class ProjectsPage : LocatablePage
	{
		public ProjectsViewModel ViewModel { get; }

		public ProjectsPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Projects)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<ProjectsViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryProjectsPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
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
