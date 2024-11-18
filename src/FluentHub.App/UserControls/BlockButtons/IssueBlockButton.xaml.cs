using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.BlockButtons
{
	public sealed partial class IssueBlockButton : UserControl
	{
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(IssueBlockButtonViewModel),
				typeof(IssueBlockButtonViewModel),
				typeof(IssueBlockButton),
				new PropertyMetadata(null));

		public IssueBlockButtonViewModel ViewModel
		{
			get => (IssueBlockButtonViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				ViewModel?.LoadContents();
			}
		}

		public IssueBlockButton()
		{
			InitializeComponent();
		}

		private void OnClick(object sender, RoutedEventArgs e)
		{
			var service = Ioc.Default.GetRequiredService<INavigationService>();

			var navBar = service.TabView.SelectedItem.NavigationBar;
			navBar.Context = new()
			{
				PrimaryText = ViewModel.IssueItem.Repository.Owner.Login,
				SecondaryText = ViewModel.IssueItem.Repository.Name,
				Number = ViewModel.IssueItem.Number,
			};

			service.Navigate<Views.Repositories.Issues.IssuePage>();
		}
	}
}
