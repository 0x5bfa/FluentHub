using FluentHub.Services;
using FluentHub.ViewModels.Searches;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Views.Searches
{
	public sealed partial class CodePage : ScreenView
	{
		public CodePage()
		{
			InitializeComponent();

			ViewModel = GetRequiredService<CodeViewModel>();
			_screenViewModel = ViewModel;
			_screenLoadCommand = ViewModel.LoadSearchCodePageCommand;
		}

		public CodeViewModel ViewModel { get; }

		protected override void OnActivated(AppRoute route)
		{
			if (route is not SearchRoute { Kind: SearchKind.Code } search)
				return;

			ViewModel.SearchTerm = search.Query;

			var command = ViewModel.LoadSearchCodePageCommand;
			if (command.CanExecute(null))
				command.ExecuteAsync(null);
		}
	}
}
