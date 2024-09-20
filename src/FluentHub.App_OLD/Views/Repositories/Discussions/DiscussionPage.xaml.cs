using FluentHub.App.ViewModels.Repositories.Discussions;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Discussions
{
	public sealed partial class DiscussionPage : LocatablePage
	{
		public DiscussionViewModel ViewModel;

		public DiscussionPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Discussions)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<DiscussionViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryDiscussionPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var param = e.Parameter as FrameNavigationParameter;
			ViewModel.Login = param.PrimaryText;
			ViewModel.Name = param.SecondaryText;
			ViewModel.Number = param.Number;

			var command = ViewModel.LoadRepositoryDiscussionPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
