using FluentHub.Data.Parameters;
using FluentHub.Services;
using FluentHub.ViewModels.UserControls.Overview;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.UserControls.Overview
{
	public sealed partial class AppSettingsOverview : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(ViewModel),
				typeof(AppSettingsOverviewViewModel),
				typeof(AppSettingsOverviewViewModel),
				new PropertyMetadata(null));

		public AppSettingsOverviewViewModel ViewModel
		{
			get => (AppSettingsOverviewViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				if (ViewModel is not null)
					SelectItemByTag(ViewModel.SelectedTag);
			}
		}
		#endregion

		public AppSettingsOverview()
		{
			InitializeComponent();

			_navigation = Ioc.Default.GetRequiredService<INavigationService>();
		}

		private readonly INavigationService _navigation;

		private void SelectItemByTag(string tag)
		{
			var defaultItem
				= SettingsNavView
				.MenuItems
				.OfType<NavigationViewItem>()
				.FirstOrDefault();

			SettingsNavView.SelectedItem
				= SettingsNavView
				.MenuItems
				.OfType<NavigationViewItem>()
				.FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
				?? defaultItem;
		}

		private void OnSettingsNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			if (args.InvokedItemContainer?.Tag is not { } tag)
				return;

			switch (tag.ToString()!.ToLowerInvariant())
			{
				case "appearance":
					_navigation.Navigate<Views.AppSettings.GeneralPage>();
					break;
			}
		}
	}
}
