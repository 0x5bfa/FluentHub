// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.ViewModels.Repositories.Issues;
using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using FluentHub.Data.Parameters;
using Microsoft.UI.Xaml;

namespace FluentHub.Views.Repositories.Issues
{
	public sealed partial class IssuesPage : LocatablePage
	{
		public IssuesViewModel ViewModel;

		public IssuesPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Issues)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<IssuesViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryIssuesPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadRepositoryIssuesPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadRepositoryIssuesFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}

		private async void OnCreateIssueClicked(object sender, RoutedEventArgs e)
		{
			var titleBox = new TextBox
			{
				Header = "Title",
				PlaceholderText = "Issue title",
			};
			var bodyBox = new TextBox
			{
				Header = "Description",
				AcceptsReturn = true,
				MinHeight = 160,
				TextWrapping = TextWrapping.Wrap,
			};
			var content = new StackPanel { Spacing = 12 };
			content.Children.Add(titleBox);
			content.Children.Add(bodyBox);

			var dialog = new ContentDialog
			{
				Title = "New issue",
				Content = content,
				PrimaryButtonText = "Create",
				CloseButtonText = "Cancel",
				DefaultButton = ContentDialogButton.Primary,
				XamlRoot = XamlRoot,
			};

			if (await dialog.ShowAsync() == ContentDialogResult.Primary)
				await ViewModel.CreateIssueAsync(titleBox.Text, bodyBox.Text);
		}
	}
}
