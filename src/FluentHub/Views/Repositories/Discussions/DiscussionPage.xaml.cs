using FluentHub.Data.Parameters;
using FluentHub.Services;
using FluentHub.ViewModels.Repositories.Discussions;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Views.Repositories.Discussions
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
			if (e.Parameter is not FrameNavigationParameter param)
				return;

			ViewModel.Login = param.PrimaryText ?? string.Empty;
			ViewModel.Name = param.SecondaryText ?? string.Empty;
			ViewModel.Number = param.Number;

			var command = ViewModel.LoadRepositoryDiscussionPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
