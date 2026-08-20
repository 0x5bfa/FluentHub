using FluentHub.Data.Parameters;
using FluentHub.Services;
using FluentHub.ViewModels.Searches;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Views.Searches
{
	public sealed partial class IssuesPage : Page
	{
		public IssuesPage()
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<IssuesViewModel>();
		}

		public IssuesViewModel ViewModel { get; }

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			if (e.Parameter is not FrameNavigationParameter { Parameters: string searchTerm })
				return;

			ViewModel.SearchTerm = searchTerm;

			var command = ViewModel.LoadSearchIssuesPageCommand;
			if (command.CanExecute(null))
				command.ExecuteAsync(null);
		}
	}
}
