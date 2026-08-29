using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Controls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls.BlockButtons
{
	public sealed partial class RepoBlockButton : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(ViewModel),
				typeof(RepoBlockButtonViewModel),
				typeof(RepoBlockButton),
				new PropertyMetadata(null));

		public RepoBlockButtonViewModel ViewModel
		{
			get => (RepoBlockButtonViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
			}
		}
		#endregion

		public RepoBlockButton()
			=> InitializeComponent();
	}
}
