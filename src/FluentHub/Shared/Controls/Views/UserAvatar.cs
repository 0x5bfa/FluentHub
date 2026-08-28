// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Shared.Controls.Views;

/// <summary>
/// Presents a GitHub user through WinUI's person-picture semantics and fallback behavior.
/// </summary>
public sealed partial class UserAvatar : UserControl
{
	private readonly PersonPicture _avatarPicture;
	private readonly TextBlock _labelTextBlock;

	public static readonly DependencyProperty AvatarUrlProperty =
		DependencyProperty.Register(
			nameof(AvatarUrl),
			typeof(string),
			typeof(UserAvatar),
			new PropertyMetadata(null, OnAvatarUrlChanged));

	public static readonly DependencyProperty DisplayNameProperty =
		DependencyProperty.Register(
			nameof(DisplayName),
			typeof(string),
			typeof(UserAvatar),
			new PropertyMetadata(null, OnIdentityChanged));

	public static readonly DependencyProperty LabelProperty =
		DependencyProperty.Register(
			nameof(Label),
			typeof(string),
			typeof(UserAvatar),
			new PropertyMetadata(null, OnIdentityChanged));

	public static readonly DependencyProperty SizeProperty =
		DependencyProperty.Register(
			nameof(Size),
			typeof(double),
			typeof(UserAvatar),
			new PropertyMetadata(20D, OnSizeChanged));

	public UserAvatar()
	{
		_avatarPicture = new PersonPicture()
		{
			IsTabStop = false,
		};
		AutomationProperties.SetAccessibilityView(_avatarPicture, AccessibilityView.Raw);

		_labelTextBlock = new()
		{
			VerticalAlignment = VerticalAlignment.Center,
			Visibility = Visibility.Collapsed,
		};

		Content = new StackPanel
		{
			Orientation = Orientation.Horizontal,
			Spacing = 8,
			Children =
			{
				_avatarPicture,
				_labelTextBlock,
			},
		};

		UpdateAvatarUrl();
		UpdateIdentity();
		UpdateSize();
	}

	public string? AvatarUrl
	{
		get => (string?)GetValue(AvatarUrlProperty);
		set => SetValue(AvatarUrlProperty, value);
	}

	public string? DisplayName
	{
		get => (string?)GetValue(DisplayNameProperty);
		set => SetValue(DisplayNameProperty, value);
	}

	public string? Label
	{
		get => (string?)GetValue(LabelProperty);
		set => SetValue(LabelProperty, value);
	}

	public double Size
	{
		get => (double)GetValue(SizeProperty);
		set => SetValue(SizeProperty, value);
	}

	internal PersonPicture Picture
		=> _avatarPicture;

	internal GitHubImageLoadStatus LoadStatus
		=> GitHubImageCache.GetLoadStatus(_avatarPicture);

	private static void OnAvatarUrlChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		=> ((UserAvatar)dependencyObject).UpdateAvatarUrl();

	private static void OnIdentityChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		=> ((UserAvatar)dependencyObject).UpdateIdentity();

	private static void OnSizeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		=> ((UserAvatar)dependencyObject).UpdateSize();

	private void UpdateAvatarUrl()
		=> GitHubImageCache.SetSource(_avatarPicture, AvatarUrl);

	private void UpdateIdentity()
	{
		var accessibleName = string.IsNullOrWhiteSpace(DisplayName) ? Label : DisplayName;
		_avatarPicture.DisplayName = accessibleName;
		AutomationProperties.SetName(_avatarPicture, accessibleName ?? string.Empty);

		_labelTextBlock.Text = Label ?? string.Empty;
		_labelTextBlock.Visibility = string.IsNullOrWhiteSpace(Label)
			? Visibility.Collapsed
			: Visibility.Visible;
	}

	private void UpdateSize()
	{
		_avatarPicture.Width = Size;
		_avatarPicture.Height = Size;
	}
}
