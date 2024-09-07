using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls
{
	public sealed partial class StepsPanelControl : UserControl
	{
		#region propdp
		public static readonly DependencyProperty NumberProperty =
			DependencyProperty.Register(
				nameof(Number),
				typeof(string),
				typeof(StepsPanelControl),
				new PropertyMetadata(null));

		public string Number
		{
			get => (string)GetValue(NumberProperty);
			set => SetValue(NumberProperty, value += ".");
		}

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register(
				nameof(Text),
				typeof(string),
				typeof(StepsPanelControl),
				new PropertyMetadata(null));

		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}
		#endregion

		public StepsPanelControl()
		{
			InitializeComponent();
		}
	}
}
