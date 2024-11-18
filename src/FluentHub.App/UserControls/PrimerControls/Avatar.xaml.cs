using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.PrimerControls
{
	/// <summary>
	/// See https://primer.style/react/Avatar
	/// </summary>
	public sealed partial class Avatar : UserControl
	{
		public static readonly DependencyProperty SourceProperty =
			DependencyProperty.Register(
				nameof(Source),
				typeof(string),
				typeof(Avatar),
				new PropertyMetadata("https://github.com/identicons/FluentHub.png"));

		public string Source
		{
			get => (string)GetValue(SourceProperty);
			set => SetValue(SourceProperty, value);
		}

		public static readonly DependencyProperty LabelProperty =
			DependencyProperty.Register(
				nameof(Label),
				typeof(string),
				typeof(Avatar),
				new PropertyMetadata(null));

		public string Label
		{
			get => (string)GetValue(LabelProperty);
			set => SetValue(LabelProperty, value);
		}

		public static readonly DependencyProperty SizeProperty =
			DependencyProperty.Register(
				nameof(Size),
				typeof(double),
				typeof(Avatar),
				new PropertyMetadata(20D));

		public double Size
		{
			get => (double)GetValue(SizeProperty);
			set => SetValue(SizeProperty, value);
		}

		public static readonly DependencyProperty IsSquareProperty =
			DependencyProperty.Register(
				nameof(IsSquare),
				typeof(bool),
				typeof(Avatar),
				new PropertyMetadata(false));

		public bool IsSquare
		{
			get => (bool)GetValue(IsSquareProperty);
			set => SetValue(IsSquareProperty, value);
		}

		public static readonly DependencyProperty IsClickableProperty =
			DependencyProperty.Register(
				nameof(IsClickable),
				typeof(bool),
				typeof(Avatar),
				new PropertyMetadata(false));

		public bool IsClickable
		{
			get => (bool)GetValue(IsClickableProperty);
			set => SetValue(IsClickableProperty, value);
		}

		public Avatar()
			=> InitializeComponent();
	}
}
