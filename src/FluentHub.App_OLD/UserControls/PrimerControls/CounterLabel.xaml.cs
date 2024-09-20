using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.PrimerControls
{
	/// <summary>
	/// For more information, visit https://primer.style/react/CounterLabel
	/// </summary>
	public sealed partial class CounterLabel : UserControl
	{
		#region propdp
		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register(
				nameof(Text),
				typeof(string),
				typeof(CounterLabel),
				new PropertyMetadata(null)
				);

		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		public static readonly DependencyProperty SchemeProperty =
			DependencyProperty.Register(
				nameof(Scheme),
				typeof(string),
				typeof(CounterLabel),
				new PropertyMetadata("primary")
				);

		public string Scheme
		{
			get => (string)GetValue(SchemeProperty);
			set => SetValue(SchemeProperty, value);
		}
		#endregion

		public CounterLabel()
		{
			InitializeComponent();
		}
	}
}
