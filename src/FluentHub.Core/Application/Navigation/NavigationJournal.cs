// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Navigation;

/// <summary>
/// Stores route-only navigation history. Views, view models, scopes, and API models must not be stored here.
/// </summary>
public sealed class NavigationJournal<TRoute>
	where TRoute : notnull
{
	private readonly List<TRoute> _entries = [];
	private int _currentIndex = -1;

	public IReadOnlyList<TRoute> Entries
		=> _entries;

	public int CurrentIndex
		=> _currentIndex;

	public TRoute? Current
		=> _currentIndex >= 0 ? _entries[_currentIndex] : default;

	public bool CanGoBack
		=> _currentIndex > 0;

	public bool CanGoForward
		=> _currentIndex >= 0 && _currentIndex < _entries.Count - 1;

	public void Navigate(TRoute route)
	{
		ArgumentNullException.ThrowIfNull(route);

		if (CanGoForward)
			_entries.RemoveRange(_currentIndex + 1, _entries.Count - _currentIndex - 1);

		_entries.Add(route);
		_currentIndex = _entries.Count - 1;
	}

	public bool TryGoBack(out TRoute? route)
	{
		if (!CanGoBack)
		{
			route = default;
			return false;
		}

		_currentIndex--;
		route = _entries[_currentIndex];
		return true;
	}

	public bool TryGoForward(out TRoute? route)
	{
		if (!CanGoForward)
		{
			route = default;
			return false;
		}

		_currentIndex++;
		route = _entries[_currentIndex];
		return true;
	}

	public NavigationJournalSnapshot CaptureSnapshot()
		=> new([.. _entries], _currentIndex);

	public void RestoreSnapshot(NavigationJournalSnapshot snapshot)
	{
		if (snapshot.CurrentIndex < -1 || snapshot.CurrentIndex >= snapshot.Entries.Count)
			throw new ArgumentOutOfRangeException(nameof(snapshot));

		_entries.Clear();
		_entries.AddRange(snapshot.Entries);
		_currentIndex = snapshot.CurrentIndex;
	}

	public readonly record struct NavigationJournalSnapshot(
		IReadOnlyList<TRoute> Entries,
		int CurrentIndex);
}
