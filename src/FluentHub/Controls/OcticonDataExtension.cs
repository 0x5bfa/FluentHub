// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;

namespace FluentHub.Controls;

public sealed partial class OcticonDataExtension : MarkupExtension
{
	public string? Name { get; set; }

	protected override object ProvideValue()
	{
		if (string.IsNullOrWhiteSpace(Name))
			throw new InvalidOperationException("OcticonData requires an icon name.");

		return Octicons.CreateGeometry(Name);
	}
}
