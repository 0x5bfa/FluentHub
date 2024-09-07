using FluentHub.App.ViewModels.Repositories.Discussions;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Discussions
{
	public sealed partial class DiscussionsPage : LocatablePage
	{
		public DiscussionsViewModel ViewModel;

		public DiscussionsPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Discussions)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<DiscussionsViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryDiscussionsFurtherCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadRepositoryDiscussionsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadRepositoryDiscussionsFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
