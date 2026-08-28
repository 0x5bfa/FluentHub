// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.ViewModels;

public abstract class ScreenViewModel : ObservableObject, IScreenViewModel<AppRoute>
{
	public virtual Task ActivateAsync(AppRoute route, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		return Task.CompletedTask;
	}

	public virtual Task ReloadAsync(CancellationToken cancellationToken)
		=> Task.CompletedTask;

	public virtual Task DeactivateAsync(CancellationToken cancellationToken)
		=> Task.CompletedTask;
}
