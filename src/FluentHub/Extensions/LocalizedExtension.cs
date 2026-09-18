// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Markup;

namespace FluentHub.Extensions;

/// <summary>
/// Resolves an application string resource for use in XAML.
/// </summary>
public sealed partial class LocalizedExtension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the resource key to resolve.
	/// </summary>
	public string ResourceKey { get; set; } = string.Empty;

	/// <inheritdoc />
	protected override object ProvideValue()
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(ResourceKey);

		return ResourceKey.GetLocalized();
	}
}
