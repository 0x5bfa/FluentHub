using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Controls.Overview;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls.Overview
{
	public sealed partial class UserProfileOverview : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(ViewModel),
				typeof(UserProfileOverviewViewModel),
				typeof(UserProfileOverview),
				new PropertyMetadata(null));

		public UserProfileOverviewViewModel ViewModel
		{
			get => (UserProfileOverviewViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}
		#endregion

		public UserProfileOverview()
		{
			InitializeComponent();
			AvatarImage.Picture.RegisterPropertyChangedCallback(
				GitHubImageCache.LoadStatusProperty,
				OnAvatarLoadStatusChanged);
			UpdateAvatarState();
			navService = Ioc.Default.GetRequiredService<INavigationService>();
		}

		private readonly INavigationService navService;

		private void OnAvatarLoadStatusChanged(DependencyObject sender, DependencyProperty dependencyProperty)
			=> UpdateAvatarState();

		private void UpdateAvatarState()
		{
			var status = AvatarImage.LoadStatus;
			var isLoading = status == GitHubImageLoadStatus.Loading;

			AvatarShimmer.IsActive = isLoading;
			AvatarShimmer.Visibility = isLoading ? Visibility.Visible : Visibility.Collapsed;
			AvatarImage.Opacity = isLoading ? 0 : 1;
		}

		private async void OnUserFollowersButtonClick(object sender, RoutedEventArgs e)
		{
			await navService.NavigateAsync(new UserRoute(ViewModel.User.Login, UserSection.Followers));
		}

		private async void OnUserFollowingButtonClick(object sender, RoutedEventArgs e)
		{
			await navService.NavigateAsync(new UserRoute(ViewModel.User.Login, UserSection.Following));
		}

		private async void OnEditProfileButtonClick(object sender, RoutedEventArgs e)
		{
			var dialog = new Views.Dialogs.UserProfileEditor(ViewModel.User.Login);

			// https://github.com/microsoft/microsoft-ui-xaml/issues/2504
			dialog.XamlRoot = this.Content.XamlRoot;

			_ = await dialog.ShowAsync();
		}

		private void LocationHyperlink()
		{
			var LocationHyperlink = "https://www.bing.com/maps?q=" + ViewModel.User.Location;
		}
	}
}
