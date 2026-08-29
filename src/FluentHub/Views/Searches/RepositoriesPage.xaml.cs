using FluentHub.Services;
using FluentHub.ViewModels.Searches;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Views.Searches
{
	public sealed partial class RepositoriesPage : ScreenView
	{
		public RepositoriesPage()
		{
			InitializeComponent();

			ViewModel = GetRequiredService<RepositoriesViewModel>();
			_screenViewModel = ViewModel;
			_screenLoadCommand = ViewModel.LoadSearchRepositoriesPageCommand;
		}

		public RepositoriesViewModel ViewModel { get; }

		protected override void OnActivated(AppRoute route)
		{
			if (route is not SearchRoute { Kind: SearchKind.Repositories } search)
				return;

			ViewModel.SearchTerm = search.Query;

			var command = ViewModel.LoadSearchRepositoriesPageCommand;
			if (command.CanExecute(null))
				command.ExecuteAsync(null);
		}
	}
}
