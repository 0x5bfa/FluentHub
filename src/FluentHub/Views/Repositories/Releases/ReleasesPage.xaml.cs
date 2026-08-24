using FluentHub.Helpers;
using FluentHub.Services;
using FluentHub.ViewModels.Repositories.Releases;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Views.Repositories.Releases
{
	public sealed partial class ReleasesPage : LocatablePage
	{
		public ReleasesViewModel ViewModel;

		public ReleasesPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<ReleasesViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryReleasesFurtherCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadRepositoryReleasesPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadRepositoryReleasesFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}

		private void ReleaseBlockButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is not Button { CommandParameter: string tag })
				return;

			var command = ViewModel.GoToReleasePageCommand;
			if (command.CanExecute(tag))
				command.Execute(tag);
		}
	}
}
