using FluentHub.Services;
using FluentHub.Features.Repositories.ViewModels;
using FluentHub.Shared.Controls.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Shared.Controls.Views
{
	public sealed partial class ReadmeContentBlock : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ContextViewModelProperty =
			DependencyProperty.Register(
				nameof(ContextViewModel),
				typeof(RepoContextViewModel),
				typeof(ReadmeContentBlock),
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
			if (sender is not ReadmeContentBlock control || args.NewValue is not RepoContextViewModel context)
				return;

			control.ViewModel.ContextViewModel = context;
			await control.ViewModel.LoadRepositoryReadmeAsync();
		}
		#endregion

		public ReadmeContentBlock()
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<ReadmeContentBlockViewModel>();
		}

		public ReadmeContentBlockViewModel ViewModel { get; }
	}
}
