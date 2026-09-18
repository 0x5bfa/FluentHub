// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI;
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
	public Avatar()
	{
		InitializeComponent();
		AutomationProperties.SetAccessibilityView(Picture, AccessibilityView.Raw);

		UpdateSource();
		UpdateIdentity();
		UpdateSize();
	}

	/// <summary>
	/// Gets or sets the avatar image URL.
	/// </summary>
	[GeneratedDependencyProperty]
	public partial string? Src { get; set; }

	/// <summary>
	/// Gets or sets the accessible name and fallback initials source.
	/// </summary>
	[GeneratedDependencyProperty]
	public partial string? Alt { get; set; }

	/// <summary>
	/// Gets or sets an already-created image source.
	/// </summary>
	[GeneratedDependencyProperty]
	public partial ImageSource? Source { get; set; }

	/// <summary>
	/// Gets or sets the avatar size in device-independent pixels.
	/// </summary>
	[GeneratedDependencyProperty(DefaultValue = 20D)]
	public partial double Size { get; set; }

	/// <summary>
	/// Gets or sets whether the avatar represents a non-human entity.
	/// </summary>
	[GeneratedDependencyProperty(DefaultValue = false)]
	public partial bool Square { get; set; }

	partial void OnSrcPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		UpdateSource();
	}

	partial void OnSourcePropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		UpdateSource();
	}

	partial void OnAltPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		UpdateIdentity();
	}

	partial void OnSizePropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		UpdateSize();
	}

	private void UpdateSource()
	{
		if (Source is not null)
		{
			Picture.ProfilePicture = Source;
			return;
		}

		if (string.IsNullOrWhiteSpace(Src) || !Uri.TryCreate(Src, UriKind.Absolute, out Uri? uri))
		{
			Picture.ProfilePicture = null;
			return;
		}

		Picture.ProfilePicture = new BitmapImage(uri);
	}

	private void UpdateIdentity()
	{
		AutomationProperties.SetName(this, Alt ?? string.Empty);
	}

	private void UpdateSize()
	{
		var size = Math.Max(1D, Size);
		Picture.Width = size;
		Picture.Height = size;
	}
}
