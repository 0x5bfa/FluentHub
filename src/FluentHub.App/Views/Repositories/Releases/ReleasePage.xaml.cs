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

		public ReleasePage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<ReleaseViewModel>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadRepositoryReleasePageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
