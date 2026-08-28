using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Controls.FeedBlocks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls.FeedBlocks
{
	public sealed partial class SingleReleaseBlock : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(ViewModel),
				typeof(SingleReleaseBlockViewModel),
				typeof(SingleReleaseBlock),
				new PropertyMetadata(null));

		public SingleReleaseBlockViewModel ViewModel
		{
			get => (SingleReleaseBlockViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				DataContext = ViewModel;
			}
		}
		#endregion

		public SingleReleaseBlock()
			=> InitializeComponent();
	}
}
