using FluentHub.App.ViewModels.UserControls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls
{
	public sealed partial class IssueCommentBlock : UserControl
	{
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(ViewModel),
				typeof(IssueCommentBlockViewModel),
				typeof(IssueCommentBlock),
				new PropertyMetadata(null));

		public IssueCommentBlockViewModel ViewModel
		{
			get => (IssueCommentBlockViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		public IssueCommentBlock()
			=> InitializeComponent();
	}
}
