using FluentHub.App.ViewModels.Searches;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Searches
{
	public sealed partial class CodePage : Page
	{
		public CodePage()
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<CodeViewModel>();
		}

		public CodeViewModel ViewModel { get; }

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var param = e.Parameter as FrameNavigationParameter;
			ViewModel.SearchTerm = param.Parameters as string;

			var command = ViewModel.LoadSearchCodePageCommand;
			if (command.CanExecute(null))
				command.ExecuteAsync(null);
		}
	}
}
