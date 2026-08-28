// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;

namespace FluentHub.Shell.Navigation;

public interface IScreen
{
	Task ActivateAsync(AppRoute route, CancellationToken cancellationToken);

	Task ReloadAsync(CancellationToken cancellationToken);

	Task DeactivateAsync(CancellationToken cancellationToken);
}

public interface IScreenViewModel<in TRoute>
	where TRoute : AppRoute
{
	Task ActivateAsync(TRoute route, CancellationToken cancellationToken);

	Task ReloadAsync(CancellationToken cancellationToken);

	Task DeactivateAsync(CancellationToken cancellationToken);
}
