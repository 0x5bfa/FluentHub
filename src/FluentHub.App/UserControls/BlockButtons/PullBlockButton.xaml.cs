using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.BlockButtons
{
	public sealed partial class PullBlockButton : UserControl
	{
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(PullRequest),
				typeof(PullBlockButtonViewModel),
				typeof(PullBlockButton),
				new PropertyMetadata(null));

		public PullBlockButtonViewModel ViewModel
		{
			get => (PullBlockButtonViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				ViewModel?.LoadContents();
			}
		}

		public PullBlockButton()
		{
			InitializeComponent();
		}

		private void OnClick(object sender, RoutedEventArgs e)
		{
			var service = Ioc.Default.GetRequiredService<INavigationService>();

			var navBar = service.TabView.SelectedItem.NavigationBar;
			navBar.Context = new()
			{
				PrimaryText = ViewModel.PullItem.Repository.Owner.Login,
				SecondaryText = ViewModel.PullItem.Repository.Name,
				Number = ViewModel.PullItem.Number,
			};

			service.Navigate<Views.Repositories.PullRequests.ConversationPage>();
		}
	}
}
