// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Globalization;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using WindowsColor = Windows.UI.Color;

namespace FluentHub.Controls;

public enum LabelVariant
{
	Default,
	Primary,
	Secondary,
	Accent,
	Success,
	Attention,
	Severe,
	Danger,
	Done,
	Sponsors,
	Custom,
}

/// <summary>
/// Displays a compact, outlined piece of contextual metadata.
/// </summary>
public sealed partial class Label : UserControl
{
	public Label()
	{
		InitializeComponent();
		ActualThemeChanged += OnActualThemeChanged;
		UpdateText();
		UpdateAppearance();
	}

	/// <summary>
	/// Gets or sets the text displayed by the label.
	/// </summary>
	[GeneratedDependencyProperty(DefaultValue = "")]
	public partial string Text { get; set; }

	/// <summary>
	/// Gets or sets the visual color variant.
	/// </summary>
	[GeneratedDependencyProperty(DefaultValue = LabelVariant.Default)]
	public partial LabelVariant Variant { get; set; }

	/// <summary>
	/// Gets or sets a hex color used when <see cref="Variant"/> is <see cref="LabelVariant.Custom"/>.
	/// Supports #RGB, #ARGB, #RRGGBB, and #AARRGGBB.
	/// </summary>
	[GeneratedDependencyProperty]
	public partial string? Color { get; set; }

	partial void OnTextPropertyChanged(DependencyPropertyChangedEventArgs e)
		=> UpdateText();

	partial void OnVariantPropertyChanged(DependencyPropertyChangedEventArgs e)
		=> UpdateAppearance();

	partial void OnColorPropertyChanged(DependencyPropertyChangedEventArgs e)
		=> UpdateAppearance();

	private void OnActualThemeChanged(FrameworkElement sender, object args)
	{
		UpdateAppearance();
	}

	private void UpdateText()
	{
		if (TextElement is null)
		{
			return;
		}

		TextElement.Text = Text ?? string.Empty;
		AutomationProperties.SetName(this, TextElement.Text);
	}

	private void UpdateAppearance()
	{
		if (Container is null || TextElement is null)
		{
			return;
		}

		var styleVariant = Variant == LabelVariant.Custom ? LabelVariant.Default : Variant;
		Container.ClearValue(Border.BorderBrushProperty);
		TextElement.ClearValue(TextBlock.ForegroundProperty);
		Container.Style = (Style)Resources[$"Label{styleVariant}BorderStyle"];
		TextElement.Style = (Style)Resources[$"Label{styleVariant}TextStyle"];

		if (Variant == LabelVariant.Custom && TryParseHexColor(Color, out var customColor))
		{
			var brush = new SolidColorBrush(customColor);
			Container.BorderBrush = brush;
			TextElement.Foreground = brush;
		}
	}

	private static bool TryParseHexColor(string? value, out WindowsColor color)
	{
		color = default;
		var hex = value?.Trim();
		if (string.IsNullOrEmpty(hex))
		{
			return false;
		}

		if (hex[0] == '#')
		{
			hex = hex[1..];
		}

		if (hex.Length is 3 or 4)
		{
			hex = string.Concat(hex.Select(static character => new string(character, 2)));
		}

		if (hex.Length == 6)
		{
			hex = "FF" + hex;
		}

		if (hex.Length != 8 ||
			!uint.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var argb))
		{
			return false;
		}

		color = WindowsColor.FromArgb(
			(byte)(argb >> 24),
			(byte)(argb >> 16),
			(byte)(argb >> 8),
			(byte)argb);
		return true;
	}
}
