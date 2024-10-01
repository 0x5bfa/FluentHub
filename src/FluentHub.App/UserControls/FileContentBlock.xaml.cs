using FluentHub.App.ViewModels.Repositories;
using FluentHub.App.ViewModels.UserControls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls
{
	public sealed partial class FileContentBlock : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ContextViewModelProperty =
			DependencyProperty.Register(
				nameof(ContextViewModel),
				typeof(RepoContextViewModel),
				typeof(FileContentBlock),
				new PropertyMetadata(null));

		public RepoContextViewModel ContextViewModel
		{
			get => (RepoContextViewModel)GetValue(ContextViewModelProperty);
			set
			{
				SetValue(ContextViewModelProperty, value);

				if (ContextViewModel != null)
				{
					ViewModel.ContextViewModel = ContextViewModel;
					ViewModel.LoadRepositoryOneContentAsync(ColorCodeBlock);
				}
			}
		}
		#endregion

		public FileContentBlock()
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<FileContentBlockViewModel>();
		}

		public FileContentBlockViewModel ViewModel { get; }

		private void OnFileContentBlockLoaded(object sender, RoutedEventArgs e)
		{
		}
	}
}
