using FluentHub.Services;
using FluentHub.Features.Repositories.ViewModels.Discussions;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Features.Repositories.Views.Discussions
{
	public sealed partial class DiscussionPage : NavigableView
	{
		public DiscussionViewModel ViewModel;

		public DiscussionPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Discussions)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<DiscussionViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryDiscussionPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			if (route is not RepositoryDiscussionRoute)
				return;

			var command = ViewModel.LoadRepositoryDiscussionPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
