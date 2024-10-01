using FluentHub.App.ViewModels.AppSettings;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.AppSettings
{
	public sealed partial class GeneralPage : LocatablePage
	{
		public GeneralViewModel ViewModel { get; }

		private readonly INavigationService _navigation;

		public GeneralPage()
			: base(NavigationPageKind.None, NavigationPageKey.None)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<GeneralViewModel>();
			_navigation = Ioc.Default.GetRequiredService<INavigationService>();

			_navigation.TabView.SelectedItem.NavigationHistory.CurrentItem.Context =
				_navigation.TabView.SelectedItem.NavigationBar.Context = new()
				{
					PrimaryText = "Settings"
				};
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadGeneralPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
