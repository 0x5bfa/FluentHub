// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using FluentHub.Services.Navigation;

namespace FluentHub.Data.Navigation;

public sealed class NavigationBarModel : ObservableObject
{
	private bool _isNavigationSuppressed;
	private NavigationBarItem? _selectedNavigationBarItem;
	private NavigationPageKind _pageKind;
	private ScreenContext _context = ScreenContext.FromRoute(new DashboardRoute());

	public ObservableCollection<NavigationBarItem> NavigationBarItems { get; } = [];

	public NavigationBarItem? SelectedNavigationBarItem
	{
		get => _selectedNavigationBarItem;
		set
		{
			if (!SetProperty(ref _selectedNavigationBarItem, value) ||
				_isNavigationSuppressed ||
				value is null)
			{
				return;
			}

			var route = NavigationRouteBuilder.WithSection(Context.Route, value.PageItemKey);
			_ = Ioc.Default.GetRequiredService<INavigationService>().NavigateAsync(route);
		}
	}

	public NavigationPageKind PageKind
	{
		get => _pageKind;
		private set
		{
			if (SetProperty(ref _pageKind, value))
				OnPropertyChanged(nameof(IsNavigationBarShown));
		}
	}

	public bool IsNavigationBarShown
		=> PageKind != NavigationPageKind.None;

	public ScreenContext Context
	{
		get => _context;
		private set => SetProperty(ref _context, value);
	}

	public void ApplyRoute(AppRoute route)
	{
		Context = ScreenContext.FromRoute(route);
		var (kind, key) = NavigationRouteBuilder.GetNavigationSelection(route);

		if (PageKind != kind)
		{
			PageKind = kind;
			NavigationBarItems.Clear();
			foreach (var item in NavigationBarFactory.Create(kind))
				NavigationBarItems.Add(item);
		}

		SelectWithoutNavigation(
			NavigationBarItems.FirstOrDefault(item => item.PageItemKey == key));
	}

	private void SelectWithoutNavigation(NavigationBarItem? item)
	{
		_isNavigationSuppressed = true;
		try
		{
			SelectedNavigationBarItem = item;
		}
		finally
		{
			_isNavigationSuppressed = false;
		}
	}
}
