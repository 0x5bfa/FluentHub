// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI;
using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls;

/// <summary>
/// Displays activity and Markdown comments on a Primer-inspired vertical timeline.
/// </summary>
public sealed partial class Timeline : UserControl
{
	public Timeline()
	{
		InitializeComponent();
	}

	[GeneratedDependencyProperty(DefaultValueCallback = nameof(CreateDefaultItems))]
	public partial IReadOnlyList<TimelineItemViewModel> ItemsSource { get; set; }

	private static IReadOnlyList<TimelineItemViewModel> CreateDefaultItems()
		=> Array.Empty<TimelineItemViewModel>();
}

public sealed partial class TimelineItemTemplateSelector : DataTemplateSelector
{
	public DataTemplate ActivityTemplate { get; set; } = default!;

	public DataTemplate CommentTemplate { get; set; } = default!;

	protected override DataTemplate SelectTemplateCore(object item)
	{
		return GetTemplate(item);
	}

	protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
	{
		return GetTemplate(item);
	}

	private DataTemplate GetTemplate(object item)
	{
		return item is TimelineItemViewModel timelineItem
			&& timelineItem.IsComment
			? CommentTemplate
			: ActivityTemplate;
	}
}
