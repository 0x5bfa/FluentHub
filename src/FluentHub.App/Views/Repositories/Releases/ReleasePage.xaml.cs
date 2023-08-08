using FluentHub.App.Extensions;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Releases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Web.WebView2.Core;
using FluentHub.App.Data.Parameters;

namespace FluentHub.App.Views.Repositories.Releases
{
	public sealed partial class ReleasePage : LocatablePage
	{
		public ReleaseViewModel ViewModel;

		private readonly INavigationService _navigation;

		public ReleasePage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<ReleaseViewModel>();
			_navigation = Ioc.Default.GetRequiredService<INavigationService>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var param = e.Parameter as FrameNavigationParameter;
			_ = param ?? throw new ArgumentNullException("param");

			ViewModel.Login = param.UserLogin;
			ViewModel.Name = param.RepositoryName;
			ViewModel.TagName = param.Parameters.ElementAtOrDefault(0) as string;

			var command = ViewModel.LoadRepositoryReleasePageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
