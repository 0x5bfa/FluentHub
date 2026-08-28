using FluentHub.Services;
using FluentHub.Features.Searches.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Features.Searches.Views
{
	public sealed partial class UsersPage : ScreenView
	{
		public UsersPage()
		{
			InitializeComponent();

			ViewModel = GetRequiredService<UsersViewModel>();
			_screenViewModel = ViewModel;
			_screenLoadCommand = ViewModel.LoadSearchUsersPageCommand;
		}

		public UsersViewModel ViewModel { get; }

		protected override void OnActivated(AppRoute route)
		{
			if (route is not SearchRoute { Kind: SearchKind.Users } search)
				return;

			ViewModel.SearchTerm = search.Query;

			var command = ViewModel.LoadSearchUsersPageCommand;
			if (command.CanExecute(null))
				command.ExecuteAsync(null);
		}
	}
}
