// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using System.ComponentModel;

namespace FluentHub.Controls;

public sealed class Blade : INotifyPropertyChanged
{
	public Blade(UIElement content, double width = 480)
	{
		if (width <= 0)
			throw new ArgumentOutOfRangeException(nameof(width));

		Content = content ?? throw new ArgumentNullException(nameof(content));
		Width = width;
	}

	public UIElement Content { get; }

	public double Width { get; private set; }

	public event PropertyChangedEventHandler? PropertyChanged;

	internal void SetWidth(double width)
	{
		if (width <= 0 || Width == width)
			return;

		Width = width;
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Width)));
	}
}
