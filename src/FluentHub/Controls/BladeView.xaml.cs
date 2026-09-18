// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.WinUI;
using System.Collections.ObjectModel;

namespace FluentHub.Controls;

public sealed partial class BladeView : UserControl
{
	private Blade? _rootBlade;

	public BladeView()
	{
		InitializeComponent();
		SizeChanged += OnBladeViewSizeChanged;
	}

	[GeneratedDependencyProperty(DefaultValue = 480D)]
	public partial double BladeWidth { get; set; }

	partial void OnBladeWidthSet(ref double propertyValue)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(propertyValue);
	}

	partial void OnBladeWidthPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		foreach (var blade in Blades)
		{
			if (!ReferenceEquals(blade, _rootBlade))
			{
				blade.Width = BladeWidth;
			}
		}

		UpdateRootWidth();
	}

	public ObservableCollection<Blade> Blades { get; } = [];

	public void Replace(UIElement content)
	{
		_rootBlade = new Blade(content, width: BladeWidth);
		Blades.Clear();
		Blades.Add(_rootBlade);
		UpdateRootWidth();
		ScrollToLatestBlade();
	}

	public void Push(UIElement source, UIElement content)
	{
		if (_rootBlade is null)
		{
			Replace(content);
			return;
		}

		var sourceBladeIndex = -1;
		for (var index = 0; index < Blades.Count; index++)
		{
			if (ReferenceEquals(Blades[index].Content, source))
			{
				sourceBladeIndex = index;
				break;
			}
		}

		if (sourceBladeIndex < 0)
		{
			return;
		}

		while (Blades.Count > sourceBladeIndex + 1)
		{
			Blades.RemoveAt(Blades.Count - 1);
		}

		Blades.Add(new Blade(content, width: BladeWidth));
		UpdateRootWidth();
		ScrollToLatestBlade();
	}

	private void ScrollToLatestBlade()
	{
		DispatcherQueue.TryEnqueue(() =>
		{
			BladeScrollViewer.ChangeView(BladeScrollViewer.ScrollableWidth, null, null);
		});
	}

	private void OnBladeViewSizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateRootWidth();
	}

	private void UpdateRootWidth()
	{
		if (_rootBlade is null)
		{
			return;
		}

		_rootBlade.Width = Blades.Count == 1 && ActualWidth > 0 ? ActualWidth : BladeWidth;
	}
}

public sealed partial class Blade : ContentControl
{
	public Blade(UIElement content, double width = 480)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);

		Content = content ?? throw new ArgumentNullException(nameof(content));
		Width = width;
	}
}
