using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Projects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Projects
{
	public sealed partial class ProjectsPage : LocatablePage
	{
		private readonly INavigationService navigationService;
		public ProjectsViewModel ViewModel { get; }

		public ProjectsPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Projects)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<ProjectsViewModel>();
			navigationService = Ioc.Default.GetRequiredService<INavigationService>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var param = e.Parameter as FrameNavigationParameter;
			ViewModel.Login = param.PrimaryText;
			ViewModel.Name = param.SecondaryText;

			var command = ViewModel.LoadRepositoryProjectsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
