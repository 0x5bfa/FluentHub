// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;

namespace FluentHub.Services.Navigation;

public interface IScreenFactory
{
	Task<ScreenInstance> CreateAsync(AppRoute route, CancellationToken cancellationToken);
}
