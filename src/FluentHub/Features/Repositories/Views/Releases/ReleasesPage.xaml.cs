using FluentHub.Helpers;
using FluentHub.Services;
using FluentHub.Features.Repositories.ViewModels.Releases;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace FluentHub.Features.Repositories.Views.Releases
{
	public sealed partial class ReleasesPage : NavigableView
	{
		public ReleasesViewModel ViewModel;

		public ReleasesPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<ReleasesViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryReleasesPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
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
