using FluentHub.App.ViewModels.Repositories;
using FluentHub.App.ViewModels.UserControls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace FluentHub.App.UserControls
{
	public sealed partial class FileNavigationBlock : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ContextViewModelProperty =
			DependencyProperty.Register(
				nameof(ContextViewModel),
				typeof(RepoContextViewModel),
				typeof(FileNavigationBlock),
				new PropertyMetadata(null));

		public RepoContextViewModel ContextViewModel
		{
			get => (RepoContextViewModel)GetValue(ContextViewModelProperty);
			set
			{
				SetValue(ContextViewModelProperty, value);
				if (ContextViewModel is not null)
					ViewModel.ContextViewModel = ContextViewModel;
			}
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
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<FileNavigationBlockViewModel>();
			navService = Ioc.Default.GetRequiredService<INavigationService>();
		}

		private readonly INavigationService navService;
		public FileNavigationBlockViewModel ViewModel { get; }
		private bool FirstSelectionComplete { get; set; }

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

		public void SetState(UIElement target, string state)
		{
			if (target != null)
			{
				AnimatedIcon.SetState(target, state);
			}
		}
		#endregion

		private void OnFileNavigationBlockLoaded(object sender, RoutedEventArgs e)
		{
			var command = ViewModel.LoadBranchNameAllCommand;
			if (command.CanExecute(null))
				command.Execute(null);

			// Default selected branch name is current branch
			BranchNameSelector.SelectedIndex = 0;
		}

		private void OnBranchSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (FirstSelectionComplete == false)
			{
				FirstSelectionComplete = true;
				return;
			}

			ViewModel.ContextViewModel.BranchName = ContextViewModel.BranchName = BranchNameSelector.SelectedItem as string;

			var objType = ViewModel.ContextViewModel.IsFile ? "blob" : "tree";
			var path = string.IsNullOrEmpty(ViewModel.ContextViewModel.Path) ? $"{ViewModel.ContextViewModel.Path}" : $"/{ViewModel.ContextViewModel.Path}";

			var param = $"{objType}/{ViewModel.ContextViewModel.BranchName}{path}";

			navService.TabView.SelectedItem.NavigationBar.Context = new()
			{
				PrimaryText = ViewModel.ContextViewModel.Repository.Owner.Login,
				SecondaryText = ViewModel.ContextViewModel.Repository.Name,
				Parameters = param
			};

			navService.Navigate<Views.Repositories.Code.DetailsLayoutView>();
		}
	}
}
