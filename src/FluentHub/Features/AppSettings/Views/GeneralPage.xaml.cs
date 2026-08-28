using FluentHub.Services;
using FluentHub.Features.AppSettings.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Features.AppSettings.Views
{
	public sealed partial class GeneralPage : NavigableView
	{
		public GeneralViewModel ViewModel { get; }

		private readonly INavigationService _navigation;

		public GeneralPage()
			: base(NavigationPageKind.None, NavigationPageKey.None)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<GeneralViewModel>();
			_navigation = GetRequiredService<INavigationService>();
			_screenViewModel = ViewModel;
			_screenLoadCommand = ViewModel.LoadGeneralPageCommand;
		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadGeneralPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
