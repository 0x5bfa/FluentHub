// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;

namespace FluentHub.Shell.Navigation;

public interface ICurrentRouteAccessor
{
	AppRoute? Current { get; }

	IDisposable Push(AppRoute route);
}

internal sealed class CurrentRouteAccessor : ICurrentRouteAccessor
{
	private readonly AsyncLocal<AppRoute?> _current = new();

	public AppRoute? Current
		=> _current.Value;

	public IDisposable Push(AppRoute route)
	{
		var previous = _current.Value;
		_current.Value = route;
		return new PopWhenDisposed(_current, previous);
	}

	private sealed class PopWhenDisposed(AsyncLocal<AppRoute?> current, AppRoute? previous) : IDisposable
	{
		private bool _isDisposed;

		public void Dispose()
		{
			if (_isDisposed)
				return;

			current.Value = previous;
			_isDisposed = true;
		}
	}
}
