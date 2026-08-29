// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls.PrimerControls;

/// <summary>
/// Displays an account or organization image when the entity is not known to be a person.
/// </summary>
public sealed partial class EntityAvatar : UserControl
{
	public static readonly DependencyProperty SourceProperty =
		DependencyProperty.Register(
			nameof(Source),
			typeof(string),
			typeof(EntityAvatar),
			new PropertyMetadata(null));

	public static readonly DependencyProperty SizeProperty =
		DependencyProperty.Register(
			nameof(Size),
			typeof(double),
			typeof(EntityAvatar),
			new PropertyMetadata(20D, OnAppearanceChanged));

	public static readonly DependencyProperty IsSquareProperty =
		DependencyProperty.Register(
			nameof(IsSquare),
			typeof(bool),
			typeof(EntityAvatar),
			new PropertyMetadata(false, OnAppearanceChanged));

	public EntityAvatar()
	{
		InitializeComponent();
		UpdateCornerRadius();
	}

	public string? Source
	{
		get => (string?)GetValue(SourceProperty);
		set => SetValue(SourceProperty, value);
	}

	public double Size
	{
		get => (double)GetValue(SizeProperty);
		set => SetValue(SizeProperty, value);
	}

	public bool IsSquare
	{
		get => (bool)GetValue(IsSquareProperty);
		set => SetValue(IsSquareProperty, value);
	}

	private static void OnAppearanceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		=> ((EntityAvatar)dependencyObject).UpdateCornerRadius();

	private void UpdateCornerRadius()
	{
		var radius = IsSquare
			? Math.Clamp(Size / 8, 6, 12)
			: Size / 2;
		AvatarBorder.CornerRadius = new(radius);
	}
}
