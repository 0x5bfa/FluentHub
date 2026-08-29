using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Controls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Application.Models;

namespace FluentHub.Controls.BlockButtons
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

		private async void OrganizationOverviewButton_Click(object sender, RoutedEventArgs e)
		{
			var service = Ioc.Default.GetRequiredService<INavigationService>();
			await service.NavigateAsync(new OrganizationRoute(ViewModel.OrgItem.Login));
		}
	}
}
