using FluentHub.App.Services;
using FluentHub.App.ViewModels.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.Dialogs
{
	public sealed partial class AccountSwitching : ContentDialog
	{
		public AccountSwitching()
			=> InitializeComponent();

		private void OnCancelButtonClick(object sender, RoutedEventArgs e)
			=> Hide();
	}
}
