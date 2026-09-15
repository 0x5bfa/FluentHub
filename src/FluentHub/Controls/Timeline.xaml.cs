// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls;

/// <summary>
/// Displays activity and Markdown comments on a Primer-inspired vertical timeline.
/// </summary>
public sealed partial class Timeline : UserControl
{
	public static readonly DependencyProperty ItemsSourceProperty =
		DependencyProperty.Register(
			nameof(ItemsSource),
			typeof(IReadOnlyList<TimelineItemViewModel>),
			typeof(Timeline),
			new PropertyMetadata(Array.Empty<TimelineItemViewModel>()));

	public Timeline()
	{
		InitializeComponent();
	}

	public IReadOnlyList<TimelineItemViewModel> ItemsSource
	{
		get => (IReadOnlyList<TimelineItemViewModel>)GetValue(ItemsSourceProperty);
		set => SetValue(ItemsSourceProperty, value);
	}
}
