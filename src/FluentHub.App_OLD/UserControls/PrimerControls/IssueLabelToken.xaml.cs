using CommunityToolkit.WinUI.UI.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace FluentHub.App.UserControls.PrimerControls
{
	/// <summary>
	/// See https://primer.style/react/Token#issuelabeltoken
	/// 
	/// TODO: - Be more flexible for size changing(e.g. TextBlock font size)
	///	   - Add support label remover button
	/// 
	/// </summary>
	public sealed partial class IssueLabelToken : UserControl
	{
		#region propdp
		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register(
				nameof(Text),
				typeof(string),
				typeof(IssueLabelToken),
				new PropertyMetadata(null)
				);

		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		public static readonly DependencyProperty FillColorProperty =
			DependencyProperty.Register(
				nameof(FillColor),
				typeof(SolidColorBrush),
				typeof(IssueLabelToken),
				new PropertyMetadata(new SolidColorBrush())
				);

		public SolidColorBrush FillColor
		{
			get => (SolidColorBrush)GetValue(FillColorProperty);
			set => SetValue(FillColorProperty, value);
		}

		public static readonly DependencyProperty SizeProperty =
			DependencyProperty.Register(
				nameof(Size),
				typeof(string),
				typeof(IssueLabelToken),
				new PropertyMetadata("medium")
				);

		public string Size
		{
			get => (string)GetValue(SizeProperty);
			set
			{
				SetValue(SizeProperty, value);

				Context.TokenHeight = value switch
				{
					"small" => 16D,
					"medium" => 20D,
					"large" => 24D,
					"xlarge" => 32D,
					"extralarge" => 32D,
					_ => 20D, // same as medium
				};

				Context.CornerRadius = new(Context.TokenHeight / 2);

				Context.Margin = value switch
				{
					"small" => new(4, 0, 4, 0),
					"medium" => new(8, 0, 8, 0),
					"large" => new(8, 0, 8, 0),
					"xlarge" => new(16, 0, 16, 0),
					"extralarge" => new(16, 0, 16, 0),
					_ => new(8, 0, 8, 0), // same as medium
				};
			}
		}

		// The property for the on remove click event
		public static readonly DependencyProperty IsClickableProperty =
			DependencyProperty.Register(
				nameof(IsRemovable),
				typeof(bool),
				typeof(IssueLabelToken),
				new PropertyMetadata(false)
				);

		public bool IsRemovable
		{
			get => (bool)GetValue(IsClickableProperty);
			set => SetValue(IsClickableProperty, value);
		}
		#endregion

		private ObservableContext Context { get; }
		private ThemeListener Listener { get; }

		public IssueLabelToken()
		{
			Context = new();
			Listener = new();
			Listener.ThemeChanged += OnAppThemeChanged;
			InitializeComponent();
		}

		private void OnAppThemeChanged(ThemeListener sender)
		{
			var theme = sender.CurrentTheme;

			Context.IsLightMode = (theme == ApplicationTheme.Light);
		}

		private class ObservableContext : ObservableObject
		{
			public ObservableContext()
			{
				IsLightMode = (Application.Current.RequestedTheme == ApplicationTheme.Light);
				TokenHeight = 20D;
				Margin = new(8, 0, 8, 0);
				CornerRadius = new(10);
			}

			private bool _isLightMode;
			public bool IsLightMode { get => _isLightMode; set => SetProperty(ref _isLightMode, value); }

			private double _tokenHeight;
			public double TokenHeight { get => _tokenHeight; set => SetProperty(ref _tokenHeight, value); }

			private Thickness _margin;
			public Thickness Margin { get => _margin; set => SetProperty(ref _margin, value); }

			private CornerRadius _cornerRadius;
			public CornerRadius CornerRadius { get => _cornerRadius; set => SetProperty(ref _cornerRadius, value); }
		}
	}
}
