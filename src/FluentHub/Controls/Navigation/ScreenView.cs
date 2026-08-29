// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;

namespace FluentHub.Controls.Navigation;

/// <summary>
/// Base class for screens hosted by a content presenter.
/// </summary>
public abstract class ScreenView : UserControl, IScreen
{
	private static readonly AsyncLocal<IServiceProvider?> ConstructionServices = new();

	protected ICommand? _screenLoadCommand;
	protected IScreenViewModel<AppRoute>? _screenViewModel;

	public AppRoute? Route { get; private set; }

	internal static IDisposable PushServices(IServiceProvider services)
	{
		ArgumentNullException.ThrowIfNull(services);

		var previous = ConstructionServices.Value;
		ConstructionServices.Value = services;
		return new RestoreServices(previous);
	}

	protected static T GetRequiredService<T>()
		where T : notnull
		=> ConstructionServices.Value is { } services
			? services.GetRequiredService<T>()
			: Ioc.Default.GetRequiredService<T>();

	public async Task ActivateAsync(AppRoute route, CancellationToken cancellationToken)
	{
		Route = route;
		cancellationToken.ThrowIfCancellationRequested();
		if (_screenViewModel is not null)
			await _screenViewModel.ActivateAsync(route, cancellationToken);

		OnActivated(route);
	}

	public virtual async Task ReloadAsync(CancellationToken cancellationToken)
	{
		if (_screenViewModel is not null)
			await _screenViewModel.ReloadAsync(cancellationToken);

		await ExecuteCommandAsync(_screenLoadCommand, cancellationToken);
	}

	public virtual Task DeactivateAsync(CancellationToken cancellationToken)
		=> _screenViewModel?.DeactivateAsync(cancellationToken) ?? Task.CompletedTask;

	protected virtual void OnActivated(AppRoute route)
	{
		_ = ExecuteCommandAsync(_screenLoadCommand, CancellationToken.None);
	}

	protected static Task ExecuteCommandAsync(ICommand? command, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();

		if (command is IAsyncRelayCommand asyncCommand && asyncCommand.CanExecute(null))
			return asyncCommand.ExecuteAsync(null);

		if (command?.CanExecute(null) is true)
			command.Execute(null);

		return Task.CompletedTask;
	}

	private sealed class RestoreServices(IServiceProvider? previous) : IDisposable
	{
		private bool _isDisposed;

		public void Dispose()
		{
			if (_isDisposed)
				return;

			ConstructionServices.Value = previous;
			_isDisposed = true;
		}
	}
}
