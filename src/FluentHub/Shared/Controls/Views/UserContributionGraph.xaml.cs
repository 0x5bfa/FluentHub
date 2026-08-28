using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Shared.Controls.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

namespace FluentHub.Shared.Controls.Views
{
	public sealed partial class UserContributionGraph : UserControl
	{
		#region propdp
		public static readonly DependencyProperty LoginProperty =
			DependencyProperty.Register(
				nameof(Login),
				typeof(string),
				typeof(UserContributionGraph),
				new PropertyMetadata(null));

		public string Login
		{
			get => (string)GetValue(LoginProperty);
			set
			{
				SetValue(LoginProperty, value);

				ViewModel.Login = value;
				_ = ViewModel.GetContributionCalendarAsync();
			}
		}
		#endregion

		public UserContributionGraphViewModel ViewModel { get; }

		public UserContributionGraph()
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<UserContributionGraphViewModel>();
		}
	}
}
