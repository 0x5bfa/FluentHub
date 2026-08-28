// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Shell.Navigation;

public sealed class ScreenInstance : IAsyncDisposable
{
	private readonly AsyncServiceScope _scope;
	private readonly CancellationTokenSource _lifetimeCancellation = new();
	private bool _isDisposed;

	internal ScreenInstance(AppRoute route, UserControl view, IScreen screen, AsyncServiceScope scope)
	{
		Route = route;
		View = view;
		Screen = screen;
		_scope = scope;
	}

	public AppRoute Route { get; }

	public UserControl View { get; }

	public IScreen Screen { get; }

	internal CancellationToken LifetimeToken
		=> _lifetimeCancellation.Token;

	public async ValueTask DisposeAsync()
	{
		if (_isDisposed)
			return;

		_isDisposed = true;
		_lifetimeCancellation.Cancel();

		try
		{
			await Screen.DeactivateAsync(CancellationToken.None);
		}
		finally
		{
			_lifetimeCancellation.Dispose();
			await _scope.DisposeAsync();
		}
	}
}
