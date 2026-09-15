// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Controls;

/// <summary>
/// Displays a GitHub user, organization, bot, or team avatar.
/// </summary>
public sealed partial class Avatar : UserControl
{
	private readonly PersonPicture _picture;

	public static readonly DependencyProperty SrcProperty =
		DependencyProperty.Register(
			nameof(Src),
			typeof(string),
			typeof(Avatar),
			new PropertyMetadata(null, OnSourceChanged));

	public static readonly DependencyProperty AltProperty =
		DependencyProperty.Register(
			nameof(Alt),
			typeof(string),
			typeof(Avatar),
			new PropertyMetadata(null, OnIdentityChanged));

	public static readonly DependencyProperty SourceProperty =
		DependencyProperty.Register(
			nameof(Source),
			typeof(ImageSource),
			typeof(Avatar),
			new PropertyMetadata(null, OnSourceChanged));

	public static readonly DependencyProperty SizeProperty =
		DependencyProperty.Register(
			nameof(Size),
			typeof(double),
			typeof(Avatar),
			new PropertyMetadata(20D, OnSizeChanged));

	public static readonly DependencyProperty SquareProperty =
		DependencyProperty.Register(
			nameof(Square),
			typeof(bool),
			typeof(Avatar),
			new PropertyMetadata(false, OnShapeChanged));

	public Avatar()
	{
		_picture = new PersonPicture
		{
			IsTabStop = false,
		};

		AutomationProperties.SetAccessibilityView(_picture, AccessibilityView.Raw);
		Content = _picture;

		UpdateSource();
		UpdateIdentity();
		UpdateSize();
		UpdateShape();
	}

	/// <summary>
	/// Gets or sets the avatar image URL.
	/// </summary>
	public string? Src
	{
		get => (string?)GetValue(SrcProperty);
		set => SetValue(SrcProperty, value);
	}

	/// <summary>
	/// Gets or sets the accessible name and fallback initials source.
	/// </summary>
	public string? Alt
	{
		get => (string?)GetValue(AltProperty);
		set => SetValue(AltProperty, value);
	}

	/// <summary>
	/// Gets or sets an already-created image source.
	/// </summary>
	public ImageSource? Source
	{
		get => (ImageSource?)GetValue(SourceProperty);
		set => SetValue(SourceProperty, value);
	}

	/// <summary>
	/// Gets or sets the avatar size in device-independent pixels.
	/// </summary>
	public double Size
	{
		get => (double)GetValue(SizeProperty);
		set => SetValue(SizeProperty, value);
	}

	/// <summary>
	/// Gets or sets whether the avatar represents a non-human entity.
	/// </summary>
	public bool Square
	{
		get => (bool)GetValue(SquareProperty);
		set => SetValue(SquareProperty, value);
	}

	private static void OnSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		=> ((Avatar)dependencyObject).UpdateSource();

	private static void OnIdentityChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		=> ((Avatar)dependencyObject).UpdateIdentity();

	private static void OnSizeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		=> ((Avatar)dependencyObject).UpdateSize();

	private static void OnShapeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		=> ((Avatar)dependencyObject).UpdateShape();

	private void UpdateSource()
	{
		if (Source is not null)
		{
			_picture.ProfilePicture = Source;
			return;
		}

		if (string.IsNullOrWhiteSpace(Src) || !Uri.TryCreate(Src, UriKind.Absolute, out Uri? uri))
		{
			_picture.ProfilePicture = null;
			return;
		}

		_picture.ProfilePicture = new BitmapImage(uri);
	}

	private void UpdateIdentity()
	{
		_picture.DisplayName = Alt;
		AutomationProperties.SetName(this, Alt ?? string.Empty);
	}

	private void UpdateSize()
	{
		var size = Math.Max(1D, Size);
		_picture.Width = size;
		_picture.Height = size;
	}

	private void UpdateShape()
		=> _picture.IsGroup = Square;
}
