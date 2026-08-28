// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.ViewModels.Repositories.PullRequests;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Core.Application.Models;

namespace FluentHub.Views.Repositories.PullRequests
{
	public sealed partial class ConversationPage : NavigableView
	{
		public ConversationViewModel ViewModel;

		public ConversationPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.PullRequests)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<ConversationViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryPullRequestConversationPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadRepositoryPullRequestConversationPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private async void OnEditPullRequestClicked(object sender, RoutedEventArgs e)
		{
			var titleBox = new TextBox
			{
				Header = "Title",
				Text = ViewModel.PullItem.Title,
			};
			var bodyBox = new TextBox
			{
				Header = "Description",
				AcceptsReturn = true,
				MinHeight = 200,
				Text = ViewModel.PullItem.Body,
				TextWrapping = TextWrapping.Wrap,
			};
			var content = new StackPanel { Spacing = 12 };
			content.Children.Add(titleBox);
			content.Children.Add(bodyBox);

			var dialog = new ContentDialog
			{
				Title = "Edit pull request",
				Content = content,
				PrimaryButtonText = "Save",
				CloseButtonText = "Cancel",
				DefaultButton = ContentDialogButton.Primary,
				XamlRoot = XamlRoot,
			};

			if (await dialog.ShowAsync() == ContentDialogResult.Primary)
				await ViewModel.UpdatePullRequestAsync(titleBox.Text, bodyBox.Text);
		}

		private async void OnReviewClicked(object sender, RoutedEventArgs e)
		{
			var reviewType = new ComboBox
			{
				Header = "Review type",
				HorizontalAlignment = HorizontalAlignment.Stretch,
				ItemsSource = new[] { "Comment", "Approve", "Request changes" },
				SelectedIndex = 0,
			};
			var bodyBox = new TextBox
			{
				Header = "Review summary",
				AcceptsReturn = true,
				MinHeight = 160,
				TextWrapping = TextWrapping.Wrap,
			};
			var content = new StackPanel { Spacing = 12 };
			content.Children.Add(reviewType);
			content.Children.Add(bodyBox);

			var dialog = new ContentDialog
			{
				Title = "Submit review",
				Content = content,
				PrimaryButtonText = "Submit",
				CloseButtonText = "Cancel",
				DefaultButton = ContentDialogButton.Primary,
				XamlRoot = XamlRoot,
			};

			if (await dialog.ShowAsync() != ContentDialogResult.Primary)
				return;

			var reviewEvent = reviewType.SelectedIndex switch
			{
				1 => PullRequestReviewEvent.Approve,
				2 => PullRequestReviewEvent.RequestChanges,
				_ => PullRequestReviewEvent.Comment,
			};
			await ViewModel.SubmitReviewAsync(reviewEvent, bodyBox.Text);
		}

		private async void OnMergeClicked(object sender, RoutedEventArgs e)
		{
			if (sender is not MenuFlyoutItem item
				|| !Enum.TryParse<PullRequestMergeMethod>(item.Tag as string, out var method))
			{
				return;
			}

			var headlineBox = new TextBox
			{
				Header = "Commit title",
				PlaceholderText = ViewModel.PullItem.Title,
			};
			var bodyBox = new TextBox
			{
				Header = "Commit message",
				AcceptsReturn = true,
				MinHeight = 120,
				TextWrapping = TextWrapping.Wrap,
			};
			var content = new StackPanel { Spacing = 12 };
			content.Children.Add(headlineBox);
			content.Children.Add(bodyBox);

			var dialog = new ContentDialog
			{
				Title = $"{item.Text}?",
				Content = content,
				PrimaryButtonText = "Merge",
				CloseButtonText = "Cancel",
				DefaultButton = ContentDialogButton.Close,
				XamlRoot = XamlRoot,
			};

			if (await dialog.ShowAsync() == ContentDialogResult.Primary)
				await ViewModel.MergeAsync(method, headlineBox.Text, bodyBox.Text);
		}
	}
}
