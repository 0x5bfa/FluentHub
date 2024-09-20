using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace FluentHub.App.UserControls.Overview
{
	public sealed partial class OrganizationProfileOverview : UserControl
	{
		private readonly INavigationService navService;

		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(ViewModel),
				typeof(OrganizationProfileOverviewViewModel),
				typeof(OrganizationProfileOverviewViewModel),
				new PropertyMetadata(null));

		public OrganizationProfileOverviewViewModel ViewModel
		{
			get => (OrganizationProfileOverviewViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		public OrganizationProfileOverview()
		{
			InitializeComponent();

			navService = Ioc.Default.GetRequiredService<INavigationService>();
		}

		private void OnVerifiedLabelTapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
			=> FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
	}
}
