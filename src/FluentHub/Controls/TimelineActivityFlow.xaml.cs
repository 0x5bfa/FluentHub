// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using FluentHub.ViewModels;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Windows.UI.Text;

namespace FluentHub.Controls;

/// <summary>
/// Renders an activity actor and its parts as one inline flow so wrapped text
/// continues below the actor instead of starting below the action column.
/// </summary>
public sealed partial class TimelineActivityFlow : UserControl
{
	public static readonly DependencyProperty AuthorNameProperty =
		DependencyProperty.Register(
			nameof(AuthorName),
			typeof(string),
			typeof(TimelineActivityFlow),
			new PropertyMetadata(string.Empty, OnFlowPropertyChanged));

	public static readonly DependencyProperty PartsProperty =
		DependencyProperty.Register(
			nameof(Parts),
			typeof(ObservableCollection<TimelineActivityPart>),
			typeof(TimelineActivityFlow),
			new PropertyMetadata(null, OnPartsChanged));

	private ObservableCollection<TimelineActivityPart>? _observedParts;

	public TimelineActivityFlow()
	{
		InitializeComponent();
		Loaded += OnLoaded;
	}

	public string AuthorName
	{
		get => (string)GetValue(AuthorNameProperty);
		set => SetValue(AuthorNameProperty, value);
	}

	public ObservableCollection<TimelineActivityPart>? Parts
	{
		get => (ObservableCollection<TimelineActivityPart>?)GetValue(PartsProperty);
		set => SetValue(PartsProperty, value);
	}

	private static void OnFlowPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
	{
		((TimelineActivityFlow)dependencyObject).Rebuild();
	}

	private static void OnPartsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
	{
		var flow = (TimelineActivityFlow)dependencyObject;
		flow.SetObservedParts(args.NewValue as ObservableCollection<TimelineActivityPart>);
		flow.Rebuild();
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		Rebuild();
	}

	private void SetObservedParts(ObservableCollection<TimelineActivityPart>? parts)
	{
		if (_observedParts is not null)
		{
			_observedParts.CollectionChanged -= OnPartsCollectionChanged;
			_observedParts = null;
		}

		if (parts is null)
		{
			return;
		}

		parts.CollectionChanged += OnPartsCollectionChanged;
		_observedParts = parts;
	}

	private void OnPartsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		Rebuild();
	}

	private void Rebuild()
	{
		if (FlowTextBlock is null)
		{
			return;
		}

		var paragraph = new Paragraph();
		var authorName = AuthorName ?? string.Empty;
		if (authorName.Length > 0)
		{
			paragraph.Inlines.Add(new Run
			{
				FontWeight = FontWeights.SemiBold,
				Text = authorName,
			});
		}

		var parts = Parts;
		if (parts is not null && parts.Count > 0)
		{
			if (authorName.Length > 0)
			{
				paragraph.Inlines.Add(new Run { Text = " " });
			}

			foreach (var part in parts)
			{
				AddPart(paragraph, part);
			}
		}

		FlowTextBlock.Blocks.Clear();
		if (paragraph.Inlines.Count > 0)
		{
			FlowTextBlock.Blocks.Add(paragraph);
		}
	}

	private void AddPart(Paragraph paragraph, TimelineActivityPart part)
	{
		switch (part.Kind)
		{
			case TimelineActivityPartKind.Label:
			case TimelineActivityPartKind.Status:
				paragraph.Inlines.Add(CreateBadge(part));
				break;

			case TimelineActivityPartKind.Link:
				if (part.Uri is null)
				{
					paragraph.Inlines.Add(CreateRun(part.Text));
					break;
				}

				var hyperlink = new Hyperlink { NavigateUri = part.Uri };
				hyperlink.Inlines.Add(new Run { Text = part.Text });
				paragraph.Inlines.Add(hyperlink);
				break;

			case TimelineActivityPartKind.Strikethrough:
				paragraph.Inlines.Add(new Run
				{
					Text = part.Text,
					TextDecorations = TextDecorations.Strikethrough,
				});
				break;

			default:
				paragraph.Inlines.Add(CreateRun(part.Text));
				break;
		}
	}

	private static Run CreateRun(string text)
	{
		return new Run { Text = text };
	}

	private static InlineUIContainer CreateBadge(TimelineActivityPart part)
	{
		var label = new Label
		{
			Text = part.Text,
			Variant = string.IsNullOrWhiteSpace(part.Color) ? LabelVariant.Default : LabelVariant.Custom,
			Color = part.Color,
            Translation = new([0, 4, 0]),
            Margin = new(0, -4, 0, 0),
        };

		return new InlineUIContainer { Child = label };
	}
}
