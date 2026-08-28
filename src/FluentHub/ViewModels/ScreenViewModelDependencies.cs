// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Utils;

namespace FluentHub.ViewModels;

public sealed class ScreenViewModelDependencies(
	IMessenger messenger,
	ILogger logger,
	INavigationService navigation,
	ICurrentRouteAccessor currentRouteAccessor)
{
	public IMessenger Messenger { get; } = messenger;

	public ILogger Logger { get; } = logger;

	public INavigationService Navigation { get; } = navigation;

	public ICurrentRouteAccessor CurrentRouteAccessor { get; } = currentRouteAccessor;
}
