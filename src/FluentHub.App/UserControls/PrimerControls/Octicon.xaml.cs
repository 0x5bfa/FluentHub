using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.PrimerControls
{
	public sealed partial class Octicon : Control
	{
		public static readonly DependencyProperty IconProperty =
			DependencyProperty.Register(
				nameof(Icon),
				typeof(OcticonKind),
				typeof(Octicon),
				new PropertyMetadata(defaultValue: 0, (d, e) => ((Octicon)d).OnIconPropertyChanged((OcticonKind)e.OldValue, (OcticonKind)e.NewValue)));

		public OcticonKind Icon
		{
			get => (OcticonKind)GetValue(IconProperty);
			set => SetValue(IconProperty, value);
		}

		//public static readonly DependencyProperty SizeProperty =
		//	DependencyProperty.Register(
		//		nameof(Size),
		//		typeof(string),
		//		typeof(Octicon),
		//		new PropertyMetadata(defaultValue: 16, (d, e) => ((Octicon)d).OnSizePropertyChanged((bool)e.OldValue, (bool)e.NewValue)));

		//public string Size
		//{
		//	get => (string)GetValue(SizeProperty);
		//	set => SetValue(SizeProperty, value);
		//}

		public Octicon()
		{
			InitializeComponent();
		}

		private void OnIconPropertyChanged(OcticonKind oldValue, OcticonKind newValue)
		{
			Style = Application.Current.Resources[Icon.ToString()] as Style;
		}

		//private void OnSizePropertyChanged(bool oldValue, bool newValue)
		//{

		//}
	}
}
