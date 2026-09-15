// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace FluentHub.Controls;

public sealed partial class BladeView : UserControl
{
	private double _bladeWidth = 480;
	private Blade? _rootBlade;

	public BladeView()
	{
		InitializeComponent();
		SizeChanged += OnBladeViewSizeChanged;
	}

	public double BladeWidth
	{
		get => _bladeWidth;
		set
		{
			if (value <= 0)
				throw new ArgumentOutOfRangeException(nameof(value));
			if (_bladeWidth == value)
				return;

			_bladeWidth = value;
			foreach (var blade in Blades)
				if (!ReferenceEquals(blade, _rootBlade))
					blade.SetWidth(value);
			UpdateRootWidth();
		}
	}

	public ObservableCollection<Blade> Blades { get; } = new();

	public void Replace(UIElement content)
	{
		_rootBlade = new Blade(content, width: _bladeWidth);
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
			return;

		while (Blades.Count > sourceBladeIndex + 1)
			Blades.RemoveAt(Blades.Count - 1);

		Blades.Add(new Blade(content, width: _bladeWidth));
		UpdateRootWidth();
		ScrollToLatestBlade();
	}

	private void ScrollToLatestBlade()
	{
		DispatcherQueue.TryEnqueue(() =>
			BladeScrollViewer.ChangeView(BladeScrollViewer.ScrollableWidth, null, null));
	}

	private void OnBladeViewSizeChanged(object sender, SizeChangedEventArgs e)
		=> UpdateRootWidth();

	private void UpdateRootWidth()
	{
		if (_rootBlade is null)
			return;

		_rootBlade.SetWidth(Blades.Count == 1 && ActualWidth > 0 ? ActualWidth : _bladeWidth);
	}
}
