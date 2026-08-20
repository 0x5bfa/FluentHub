using FluentHub.Services;
using FluentHub.ViewModels.AppSettings;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Views.AppSettings
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

			var selectedItem = _navigation.TabView.SelectedItem;
			var context = new FrameNavigationParameter
				{
					PrimaryText = "Settings"
				};

			selectedItem.NavigationBar.Context = context;
			if (selectedItem.NavigationHistory.CurrentItem is { } currentItem)
				currentItem.Context = context;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadGeneralPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
