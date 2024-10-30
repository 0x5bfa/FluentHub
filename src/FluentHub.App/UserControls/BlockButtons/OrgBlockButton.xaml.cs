using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.BlockButtons
{
	public sealed partial class OrgBlockButton : UserControl
	{
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(Organization),
				typeof(IssueBlockButtonViewModel),
				typeof(OrgBlockButton),
				new PropertyMetadata(null));

		public OrgBlockButtonViewModel ViewModel
		{
			get => (OrgBlockButtonViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		public OrgBlockButton()
		{
			InitializeComponent();
		}

		private void OrganizationOverviewButton_Click(object sender, RoutedEventArgs e)
		{
			var service = Ioc.Default.GetRequiredService<INavigationService>();

			var navBar = service.TabView.SelectedItem.NavigationBar;
			navBar.Context = new()
			{
				PrimaryText = ViewModel.OrgItem.Login,
			};

			service.Navigate<Views.Organizations.OverviewPage>();
		}
	}
}
