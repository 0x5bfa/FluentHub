using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.PrimerControls
{
	/// <summary>
	/// See https://primer.style/react/Label
	/// If you'are looking for issue label control, see Token control
	/// </summary>
	public sealed partial class Label : UserControl
	{
		#region propdp
		public static readonly DependencyProperty VariantProperty =
			DependencyProperty.Register(
				nameof(Variant),
				typeof(string),
				typeof(Label),
				new PropertyMetadata("default")
				);

		public string Variant
		{
			get => (string)GetValue(VariantProperty);
			set => SetValue(VariantProperty, value);
		}

		public static readonly DependencyProperty SizeProperty =
			DependencyProperty.Register(
				nameof(Size),
				typeof(string),
				typeof(Label),
				new PropertyMetadata("small")
				);

		public string Size
		{
			get => (string)GetValue(SizeProperty);
			set => SetValue(SizeProperty, value);
		}

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register(
				nameof(Text),
				typeof(string),
				typeof(Label),
				new PropertyMetadata(null)
				);

		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}
		#endregion

		public Label()
			=> InitializeComponent();
	}
}
