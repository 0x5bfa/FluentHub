using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Discussions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Discussions
{
	public sealed partial class DiscussionsPage : LocatablePage
	{
		public DiscussionsViewModel ViewModel;

		private readonly INavigationService _navigation;

		public DiscussionsPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Discussions)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<DiscussionsViewModel>();
			_navigation = Ioc.Default.GetRequiredService<INavigationService>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var param = e.Parameter as FrameNavigationParameter;
			ViewModel.Login = param.UserLogin;
			ViewModel.Name = param.RepositoryName;

			var command = ViewModel.LoadRepositoryDiscussionsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
