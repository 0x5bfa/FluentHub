using FluentHub.App.ViewModels.UserControls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls
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

			ViewModel = new();
		}
	}
}
