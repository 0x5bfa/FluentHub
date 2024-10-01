using FluentHub.App.ViewModels.UserControls.FeedBlocks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.FeedBlocks
{
	public sealed partial class ActivityBlock : UserControl
	{
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
			   nameof(ViewModel),
			   typeof(ActivityBlockViewModel),
			   typeof(ActivityBlock),
			   new PropertyMetadata(null));

		public ActivityBlockViewModel ViewModel
		{
			get => (ActivityBlockViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				ViewModel?.LoadContentsAsync();
			}
		}

		public ActivityBlock()
		{
			InitializeComponent();
		}

		private void OnActivityRepositoryButtonClick(object sender, RoutedEventArgs e)
		{
			Repository repo = ((Button)sender).Tag as Repository;

			var service = Ioc.Default.GetRequiredService<INavigationService>();

			var navBar = service.TabView.SelectedItem.NavigationBar;
			navBar.Context = new()
			{
				PrimaryText = repo.Owner.Login,
				SecondaryText = repo.Name,
			};

			if (App.AppSettings.UseDetailsView)
				service.Navigate<Views.Repositories.Code.DetailsLayoutView>();
			else
				service.Navigate<Views.Repositories.Code.TreeLayoutView>();
		}
	}
}
