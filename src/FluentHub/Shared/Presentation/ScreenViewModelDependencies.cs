// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Utils;

namespace FluentHub.Shared.Presentation;

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
