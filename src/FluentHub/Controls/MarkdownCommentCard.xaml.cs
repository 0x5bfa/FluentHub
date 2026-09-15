// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls;

/// <summary>
/// Displays GitHub-style Markdown content with its author and timestamp.
/// </summary>
public sealed partial class MarkdownCommentCard : UserControl
{
	public static readonly DependencyProperty AuthorNameProperty =
		DependencyProperty.Register(
			nameof(AuthorName),
			typeof(string),
			typeof(MarkdownCommentCard),
			new PropertyMetadata("Unknown user"));

	public static readonly DependencyProperty AvatarUrlProperty =
		DependencyProperty.Register(
			nameof(AvatarUrl),
			typeof(string),
			typeof(MarkdownCommentCard),
			new PropertyMetadata(null));

	public static readonly DependencyProperty DateTextProperty =
		DependencyProperty.Register(
			nameof(DateText),
			typeof(string),
			typeof(MarkdownCommentCard),
			new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty ActionTextProperty =
		DependencyProperty.Register(
			nameof(ActionText),
			typeof(string),
			typeof(MarkdownCommentCard),
			new PropertyMetadata("commented"));

	public static readonly DependencyProperty BodyProperty =
		DependencyProperty.Register(
			nameof(Body),
			typeof(string),
			typeof(MarkdownCommentCard),
			new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty IsEditedProperty =
		DependencyProperty.Register(
			nameof(IsEdited),
			typeof(bool),
			typeof(MarkdownCommentCard),
			new PropertyMetadata(false));

	public MarkdownCommentCard()
	{
		InitializeComponent();
	}

	public string AuthorName
	{
		get => (string)GetValue(AuthorNameProperty);
		set => SetValue(AuthorNameProperty, value);
	}

	public string? AvatarUrl
	{
		get => (string?)GetValue(AvatarUrlProperty);
		set => SetValue(AvatarUrlProperty, value);
	}

	public string DateText
	{
		get => (string)GetValue(DateTextProperty);
		set => SetValue(DateTextProperty, value);
	}

	public string ActionText
	{
		get => (string)GetValue(ActionTextProperty);
		set => SetValue(ActionTextProperty, value);
	}

	public string Body
	{
		get => (string)GetValue(BodyProperty);
		set => SetValue(BodyProperty, value);
	}

	public bool IsEdited
	{
		get => (bool)GetValue(IsEditedProperty);
		set => SetValue(IsEditedProperty, value);
	}
}
