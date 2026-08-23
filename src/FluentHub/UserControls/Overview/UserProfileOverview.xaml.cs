using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.UserControls.Overview;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.UserControls.Overview
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
			AvatarImage.RegisterPropertyChangedCallback(
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
			var status = GitHubImageCache.GetLoadStatus(AvatarImage);
			var isLoading = status == GitHubImageLoadStatus.Loading;
			var isLoaded = status == GitHubImageLoadStatus.Loaded;

			AvatarShimmer.IsActive = isLoading;
			AvatarShimmer.Visibility = isLoading ? Visibility.Visible : Visibility.Collapsed;
			AvatarFallback.Visibility = status is GitHubImageLoadStatus.Empty or GitHubImageLoadStatus.Failed
				? Visibility.Visible
				: Visibility.Collapsed;
			AvatarImage.Opacity = isLoaded ? 1 : 0;
		}

		private void OnUserFollowersButtonClick(object sender, RoutedEventArgs e)
		{
			navService.Navigate<Views.Users.FollowersPage>(
			new FrameNavigationParameter()
			{
				PrimaryText = ViewModel.User.Login,
			});
		}

		private void OnUserFollowingButtonClick(object sender, RoutedEventArgs e)
		{
			navService.Navigate<Views.Users.FollowingPage>(
			new FrameNavigationParameter()
			{
				PrimaryText = ViewModel.User.Login,
			});
		}

		private async void OnEditProfileButtonClick(object sender, RoutedEventArgs e)
		{
			var dialog = new Dialogs.UserProfileEditor(ViewModel.User.Login);

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
