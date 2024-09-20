using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace FluentHub.App.UserControls.PrimerControls
{
	public sealed partial class StateLabel : UserControl
	{
		#region propdp
		public static readonly DependencyProperty StatusProperty =
			DependencyProperty.Register(
				nameof(Status),
				typeof(string),
				typeof(StateLabel),
				new PropertyMetadata(null)
				);

		public string Status
		{
			get => (string)GetValue(StatusProperty);
			set
			{
				SetValue(StatusProperty, value);

				switch (value)
				{
					default:
					case "IssueClosed":
						Context.StatusColor = Application.Current.Resources["PrimerDoneEmphasis"] as SolidColorBrush;
						Context.StatusText = "Closed";
						Context.StatusGlyph = "\uE9E6";
						break;
					case "IssueClosedNotPlanned":
						Context.StatusColor = Application.Current.Resources["PrimerNeutralEmphasis"] as SolidColorBrush;
						Context.StatusText = "Closed";
						Context.StatusGlyph = "\uE984";
						break;
					case "IssueDraft":
						Context.StatusColor = Application.Current.Resources["PrimerNeutralEmphasis"] as SolidColorBrush;
						Context.StatusText = "Draft";
						Context.StatusGlyph = "\uE9E8";
						break;
					case "IssueOpen":
						Context.StatusColor = Application.Current.Resources["PrimerSuccessEmphasis"] as SolidColorBrush;
						Context.StatusText = "Open";
						Context.StatusGlyph = "\uE9EA";
						break;
					case "PullClosed":
						Context.StatusColor = Application.Current.Resources["PrimerDangerEmphasis"] as SolidColorBrush;
						Context.StatusText = "Closed";
						Context.StatusGlyph = "\uE9C1";
						break;
					case "PullDraft":
						Context.StatusColor = Application.Current.Resources["PrimerNeutralEmphasis"] as SolidColorBrush;
						Context.StatusText = "Draft";
						Context.StatusGlyph = "\uE9C3";
						break;
					case "PullMerged":
						Context.StatusColor = Application.Current.Resources["PrimerDoneEmphasis"] as SolidColorBrush;
						Context.StatusText = "Merged";
						Context.StatusGlyph = "\uE9BD";
						break;
					case "PullOpen":
						Context.StatusColor = Application.Current.Resources["PrimerSuccessEmphasis"] as SolidColorBrush;
						Context.StatusText = "Open";
						Context.StatusGlyph = "\uE9BF";
						break;
				}
			}
		}

		public static readonly DependencyProperty VariantProperty =
			DependencyProperty.Register(
				nameof(Variant),
				typeof(string),
				typeof(StateLabel),
				new PropertyMetadata("normal")
				);

		public string Variant
		{
			get => (string)GetValue(VariantProperty);
			set
			{
				SetValue(VariantProperty, value.ToLower());

				switch (Variant)
				{
					default:
					case "normal":
						Context.Padding = new(12, 2, 12, 2);
						break;
					case "small":
						Context.Padding = new(2, 1, 2, 1);
						break;
				}
			}
		}
		#endregion

		private ObservableContext Context { get; }

		public StateLabel()
		{
			Context = new();
			InitializeComponent();
		}

		private class ObservableContext : ObservableObject
		{
			public ObservableContext()
			{
				Padding = new(12, 2, 12, 2);
				StatusColor = new();
			}

			private string _statusGlyph;
			public string StatusGlyph { get => _statusGlyph; set => SetProperty(ref _statusGlyph, value); }

			private string _statusText;
			public string StatusText { get => _statusText; set => SetProperty(ref _statusText, value); }

			private Thickness _padding;
			public Thickness Padding { get => _padding; set => SetProperty(ref _padding, value); }

			private SolidColorBrush _statusColor;
			public SolidColorBrush StatusColor { get => _statusColor; set => SetProperty(ref _statusColor, value); }
		}
	}
}
