// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls;

/// <summary>
/// Displays GitHub-style Markdown content with its author and timestamp.
/// </summary>
public sealed partial class MarkdownCommentCard : UserControl
{
	public MarkdownCommentCard()
	{
		InitializeComponent();
	}

	[GeneratedDependencyProperty(DefaultValueCallback = nameof(CreateDefaultAuthorName))]
	public partial string AuthorName { get; set; }

	[GeneratedDependencyProperty]
	public partial string? AvatarUrl { get; set; }

	[GeneratedDependencyProperty(DefaultValue = "")]
	public partial string DateText { get; set; }

	[GeneratedDependencyProperty(DefaultValueCallback = nameof(CreateDefaultActionText))]
	public partial string ActionText { get; set; }

	[GeneratedDependencyProperty(DefaultValue = "")]
	public partial string Body { get; set; }

	[GeneratedDependencyProperty(DefaultValue = false)]
	public partial bool IsEdited { get; set; }

	private static string CreateDefaultAuthorName()
		=> FluentHub.Localization.LocalizationExtensions.GetLocalized(Strings.Common_UnknownUser);

	private static string CreateDefaultActionText()
		=> FluentHub.Localization.LocalizationExtensions.GetLocalized(Strings.Common_Commented);
}
