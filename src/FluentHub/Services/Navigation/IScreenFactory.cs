// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;

namespace FluentHub.Services.Navigation;

public interface IScreenFactory
{
	Task<ScreenInstance> CreateAsync(AppRoute route, CancellationToken cancellationToken);
}
