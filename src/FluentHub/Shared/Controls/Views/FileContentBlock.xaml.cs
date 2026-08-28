using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Features.Repositories.ViewModels;
using FluentHub.Shared.Controls.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Shared.Controls.Views
{
	public sealed partial class FileContentBlock : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ContextViewModelProperty =
			DependencyProperty.Register(
				nameof(ContextViewModel),
				typeof(RepoContextViewModel),
				typeof(FileContentBlock),
				new PropertyMetadata(null, OnContextViewModelChanged));

		public RepoContextViewModel? ContextViewModel
		{
			get => (RepoContextViewModel?)GetValue(ContextViewModelProperty);
			set => SetValue(ContextViewModelProperty, value);
		}

		private static async void OnContextViewModelChanged(
			DependencyObject sender,
			DependencyPropertyChangedEventArgs args)
		{
			if (sender is not FileContentBlock control || args.NewValue is not RepoContextViewModel context)
				return;

			control.ViewModel.ContextViewModel = context;
			try
			{
				await control.ViewModel.LoadRepositoryOneContentAsync(control.ColorCodeBlock);
			}
			catch
			{
				// The view model logs failures; dependency-property callbacks cannot return a Task.
			}
		}
		#endregion

		public FileContentBlock()
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<FileContentBlockViewModel>();
		}

		public FileContentBlockViewModel ViewModel { get; }

	}
}
