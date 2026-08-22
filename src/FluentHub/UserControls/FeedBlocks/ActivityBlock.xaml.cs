using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.UserControls.FeedBlocks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Contracts;

namespace FluentHub.UserControls.FeedBlocks
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
			if (sender is not Button { Tag: Repository repo } || repo.Owner is null)
				return;

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
