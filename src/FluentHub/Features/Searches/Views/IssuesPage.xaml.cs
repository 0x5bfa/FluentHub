using FluentHub.Services;
using FluentHub.Features.Searches.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Features.Searches.Views
{
	public sealed partial class IssuesPage : ScreenView
	{
		public IssuesPage()
		{
			InitializeComponent();

			ViewModel = GetRequiredService<IssuesViewModel>();
			_screenViewModel = ViewModel;
			_screenLoadCommand = ViewModel.LoadSearchIssuesPageCommand;
		}

		public IssuesViewModel ViewModel { get; }

		protected override void OnActivated(AppRoute route)
		{
			if (route is not SearchRoute { Kind: SearchKind.Issues } search)
				return;

			ViewModel.SearchTerm = search.Query;

			var command = ViewModel.LoadSearchIssuesPageCommand;
			if (command.CanExecute(null))
				command.ExecuteAsync(null);
		}
	}
}
