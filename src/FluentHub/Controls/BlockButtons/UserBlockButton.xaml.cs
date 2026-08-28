using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Controls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Application.Models;

namespace FluentHub.Controls.BlockButtons
{
	public sealed partial class UserBlockButton : UserControl
	{
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(User),
				typeof(UserBlockButtonViewModel),
				typeof(UserBlockButton),
				new PropertyMetadata(null));

		public UserBlockButtonViewModel ViewModel
		{
			get => (UserBlockButtonViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		public UserBlockButton()
		{
			InitializeComponent();
		}

		private async void UserBlockButtonButton_Click(object sender, RoutedEventArgs e)
		{
			var service = Ioc.Default.GetRequiredService<INavigationService>();

			if (ViewModel.User.Id.ToString().StartsWith("O_"))
			{
				await service.NavigateAsync(new OrganizationRoute(ViewModel.User.Login));
			}
			else
			{
				await service.NavigateAsync(new UserRoute(ViewModel.User.Login));
			}
		}
	}
}
