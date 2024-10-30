using FluentHub.App.ViewModels.Repositories.Projects;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Projects
{
	public sealed partial class ProjectPage : LocatablePage
	{
		private readonly INavigationService _navigation;
		public ProjectViewModel ViewModel { get; }

		public ProjectPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Projects)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<ProjectViewModel>();
			_navigation = Ioc.Default.GetRequiredService<INavigationService>();
			_pageLoadCommand = ViewModel.LoadRepositoryProjectPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var param = e.Parameter as FrameNavigationParameter;
			ViewModel.Login = param.PrimaryText;
			ViewModel.Name = param.SecondaryText;
			ViewModel.Number = param.Number;

			var command = ViewModel.LoadRepositoryProjectPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
