using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.BlockButtons
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

		private void UserBlockButtonButton_Click(object sender, RoutedEventArgs e)
		{
			var service = Ioc.Default.GetRequiredService<INavigationService>();

			var navBar = service.TabView.SelectedItem.NavigationBar;
			navBar.Context = new()
			{
				PrimaryText = ViewModel.User.Login,
			};

			if (ViewModel.User.Id.ToString().StartsWith("O_"))
			{
				// Organization
				service.Navigate<Views.Organizations.OverviewPage>();
			}
			else
			{
				// User
				service.Navigate<Views.Users.OverviewPage>();
			}
		}
	}
}
