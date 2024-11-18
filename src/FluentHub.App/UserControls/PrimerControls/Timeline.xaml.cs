using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace FluentHub.App.UserControls.PrimerControls
{
	public sealed partial class Timeline : UserControl
	{
		#region propdp
		public static readonly DependencyProperty BadgeProperty =
			DependencyProperty.Register(
				nameof(Badge),
				typeof(IconElement),
				typeof(Timeline),
				new PropertyMetadata(null)
				);

		public IconElement Badge
		{
			get => (IconElement)GetValue(BadgeProperty);
			set => SetValue(BadgeProperty, value);
		}

		public static readonly DependencyProperty BadgeBackgroundProperty =
			DependencyProperty.Register(
				nameof(BadgeBackground),
				typeof(SolidColorBrush),
				typeof(Timeline),
				new PropertyMetadata((SolidColorBrush)App.Current.Resources["PrimerBorderMuted"])
				);

		public SolidColorBrush BadgeBackground
		{
			get => (SolidColorBrush)GetValue(BadgeBackgroundProperty);
			set => SetValue(BadgeBackgroundProperty, value);
		}

		public static readonly DependencyProperty BodyProperty =
			DependencyProperty.Register(
				nameof(Body),
				typeof(FrameworkElement),
				typeof(Timeline),
				new PropertyMetadata(null)
				);

		public FrameworkElement Body
		{
			get => (FrameworkElement)GetValue(BodyProperty);
			set => SetValue(BodyProperty, value);
		}

		public static readonly DependencyProperty IsCondensedProperty =
			DependencyProperty.Register(
				nameof(IsCondensed),
				typeof(bool),
				typeof(Timeline),
				new PropertyMetadata(false)
				);

		public bool IsCondensed
		{
			get => (bool)GetValue(IsCondensedProperty);
			set => SetValue(IsCondensedProperty, value);
		}
		#endregion

		public Timeline()
			=> InitializeComponent();
	}
}
