using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.UserControls.FeedBlocks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.UserControls.FeedBlocks
{
	public sealed partial class SingleCommitBlock : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(ViewModel),
				typeof(SingleCommitBlockViewModel),
				typeof(SingleCommitBlock),
				new PropertyMetadata(null));

		public SingleCommitBlockViewModel ViewModel
		{
			get => (SingleCommitBlockViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				DataContext = ViewModel;
			}
		}
		#endregion

		public SingleCommitBlock()
			=> InitializeComponent();
	}
}
