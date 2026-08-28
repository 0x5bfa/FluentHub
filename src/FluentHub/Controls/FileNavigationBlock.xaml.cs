using FluentHub.Views.Dialogs;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace FluentHub.Controls
{
	public sealed partial class FileNavigationBlock : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ContextViewModelProperty =
			DependencyProperty.Register(
				nameof(ContextViewModel),
				typeof(RepoContextViewModel),
				typeof(FileNavigationBlock),
				new PropertyMetadata(null, OnContextViewModelChanged));

		public RepoContextViewModel ContextViewModel
		{
			get => (RepoContextViewModel)GetValue(ContextViewModelProperty);
			set => SetValue(ContextViewModelProperty, value);
		}

		public static readonly DependencyProperty BranchesTotalCountProperty =
			DependencyProperty.Register(
				nameof(BranchesTotalCount),
				typeof(int),
				typeof(FileNavigationBlock),
				new PropertyMetadata(0));

		public int BranchesTotalCount
		{
			get => (int)GetValue(BranchesTotalCountProperty);
			set
			{
				SetValue(BranchesTotalCountProperty, value);
			}
		}

		public static readonly DependencyProperty TagsTotalCountProperty =
			DependencyProperty.Register(
				nameof(TagsTotalCount),
				typeof(int),
				typeof(FileNavigationBlock),
				new PropertyMetadata(0));

		public int TagsTotalCount
		{
			get => (int)GetValue(TagsTotalCountProperty);
			set
			{
				SetValue(TagsTotalCountProperty, value);
			}
		}
		#endregion

		public FileNavigationBlock()
		{
			ViewModel = Ioc.Default.GetRequiredService<FileNavigationBlockViewModel>();
			navService = Ioc.Default.GetRequiredService<INavigationService>();
			InitializeComponent();
		}

		private readonly INavigationService navService;
		public FileNavigationBlockViewModel ViewModel { get; }

		private static void OnContextViewModelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			if (sender is FileNavigationBlock control && args.NewValue is RepoContextViewModel context)
				control.ViewModel.ContextViewModel = context;
		}

		#region Chevron Amination
		private void OnCloneButtonLoaded(object sender, RoutedEventArgs e)
		{
			var button = (Button)sender;
			button.AddHandler(PointerPressedEvent, new PointerEventHandler(OnCloneButtonPointerPressed), true);
			button.AddHandler(PointerReleasedEvent, new PointerEventHandler(OnCloneButtonPointerReleased), true);
		}

		private void OnCloneButtonPointerPressed(object sender, PointerRoutedEventArgs e)
			=> SetState(sender as UIElement, "Pressed");

		private void OnCloneButtonPointerReleased(object sender, PointerRoutedEventArgs e)
			=> SetState(sender as UIElement, "Normal");

		public void SetState(UIElement? target, string state)
		{
			if (target != null)
			{
				AnimatedIcon.SetState(target, state);
			}
		}
		#endregion

		private async void OnFileNavigationBlockLoaded(object sender, RoutedEventArgs e)
		{
			if (await EnsureReferencesLoadedAsync())
				PopulateBranchSelector();
		}

		private async void OnBranchSelectorFlyoutOpening(object sender, object args)
		{
			if (await EnsureReferencesLoadedAsync())
				PopulateBranchSelector();
		}

		private async Task<bool> EnsureReferencesLoadedAsync()
		{
			if (GetValue(ContextViewModelProperty) is not RepoContextViewModel)
				return false;

			try
			{
				await ViewModel.EnsureReferencesLoadedAsync();
				BranchesTotalCount = ViewModel.BranchNames.Count;
				TagsTotalCount = ViewModel.TagNames.Count;
				return true;
			}
			catch
			{
				return false;
			}
		}

		private void PopulateBranchSelector()
		{
			var separatorIndex = BranchSelectorFlyout.Items.IndexOf(BranchSelectorSeparator);
			while (separatorIndex > 0)
			{
				BranchSelectorFlyout.Items.RemoveAt(0);
				separatorIndex--;
			}

			if (ViewModel.BranchNames.Count == 0)
			{
				BranchSelectorFlyout.Items.Insert(0, new MenuFlyoutItem
				{
					IsEnabled = false,
					Text = "No branches found",
				});
				return;
			}

			var index = 0;
			foreach (var branch in ViewModel.BranchNames.Take(10))
			{
				var item = new MenuFlyoutItem
				{
					Tag = branch,
					Text = branch,
				};
				item.Click += OnBranchMenuItemClick;
				BranchSelectorFlyout.Items.Insert(index++, item);
			}
		}

		private void OnBranchMenuItemClick(object sender, RoutedEventArgs args)
		{
			if (sender is MenuFlyoutItem { Tag: string branch })
				NavigateToReference(branch);
		}

		private async void OnViewAllBranchesClick(object sender, RoutedEventArgs args)
		{
			await Task.Yield();
			await ShowReferencesDialogAsync(RepositoryReferenceKind.Branch);
		}

		private async void OnBranchesClick(object sender, RoutedEventArgs args)
			=> await ShowReferencesDialogAsync(RepositoryReferenceKind.Branch);

		private async void OnTagsClick(object sender, RoutedEventArgs args)
			=> await ShowReferencesDialogAsync(RepositoryReferenceKind.Tag);

		private async Task ShowReferencesDialogAsync(RepositoryReferenceKind initialKind)
		{
			if (!await EnsureReferencesLoadedAsync())
				return;

			var dialog = new RepositoryRefsDialog(
				ViewModel.BranchNames,
				ViewModel.TagNames,
				ContextViewModel.BranchName,
				initialKind)
			{
				XamlRoot = XamlRoot,
			};

			if (await dialog.ShowAsync() == ContentDialogResult.Primary &&
				dialog.SelectedReference is { } reference)
			{
				NavigateToReference(reference);
			}
		}

		private async void NavigateToReference(string reference)
		{
			if (string.IsNullOrWhiteSpace(reference) ||
				reference.Equals(ContextViewModel.BranchName, StringComparison.Ordinal))
			{
				return;
			}

			ViewModel.ContextViewModel.BranchName = ContextViewModel.BranchName = reference;

			var context = ViewModel.ContextViewModel;
			var repository = new RepositorySlug(context.Repository.Owner.Login, context.Repository.Name);
			var target = context.IsFile ? RepositoryCodeTarget.File : RepositoryCodeTarget.Directory;
			await navService.NavigateAsync(
				new RepositoryCodeRoute(repository, reference, context.Path, Target: target));
		}
	}
}
