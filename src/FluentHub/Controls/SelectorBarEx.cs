// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Specialized;

namespace FluentHub.Controls
{
	public sealed partial class SelectorBarEx : SelectorBar
	{
		public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
			nameof(ItemsSource),
			typeof(ObservableCollection<NavigationBarItem>),
			typeof(SelectorBarEx),
			new PropertyMetadata(null, OnItemsSourceChanged));

		public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register(
			nameof(SelectedValue),
			typeof(NavigationBarItem),
			typeof(SelectorBarEx),
			new PropertyMetadata(null, OnSelectedValueChanged));

		private INotifyCollectionChanged? _observableItemsSource;

		private bool _isLoaded;

		private bool _isSynchronizingSelection;

		public SelectorBarEx()
		{
			Loaded += OnLoaded;
			Unloaded += OnUnloaded;
			SelectionChanged += OnSelectionChanged;
		}

		public ObservableCollection<NavigationBarItem>? ItemsSource
		{
			get => (ObservableCollection<NavigationBarItem>?)GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}

		public NavigationBarItem? SelectedValue
		{
			get => (NavigationBarItem?)GetValue(SelectedValueProperty);
			set => SetValue(SelectedValueProperty, value);
		}

		private static void OnItemsSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var selectorBar = (SelectorBarEx)dependencyObject;
			selectorBar.SubscribeToItemsSource(selectorBar._isLoaded ? args.NewValue as INotifyCollectionChanged : null);
			selectorBar.RebuildItems();
		}

		private static void OnSelectedValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
			=> ((SelectorBarEx)dependencyObject).SynchronizeSelection();

		private void OnLoaded(object sender, RoutedEventArgs args)
		{
			_isLoaded = true;
			SubscribeToItemsSource(ItemsSource);
			RebuildItems();
		}

		private void OnUnloaded(object sender, RoutedEventArgs args)
		{
			_isLoaded = false;
			SubscribeToItemsSource(null);
		}

		private void SubscribeToItemsSource(INotifyCollectionChanged? itemsSource)
		{
			if (ReferenceEquals(_observableItemsSource, itemsSource))
				return;

			if (_observableItemsSource is not null)
				_observableItemsSource.CollectionChanged -= OnItemsSourceCollectionChanged;

			_observableItemsSource = itemsSource;

			if (_observableItemsSource is not null)
				_observableItemsSource.CollectionChanged += OnItemsSourceCollectionChanged;
		}

		private void OnItemsSourceCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
			=> RebuildItems();

		private void RebuildItems()
		{
			_isSynchronizingSelection = true;

			try
			{
				Items.Clear();

				if (ItemsSource is not null)
				{
					foreach (var item in ItemsSource)
					{
						Items.Add(new SelectorBarItem()
						{
							Tag = item,
							Text = item.Text ?? string.Empty,
						});
					}
				}

				SelectedItem = FindSelectedItem();
			}
			finally
			{
				_isSynchronizingSelection = false;
			}
		}

		private void SynchronizeSelection()
		{
			_isSynchronizingSelection = true;

			try
			{
				SelectedItem = FindSelectedItem();
			}
			finally
			{
				_isSynchronizingSelection = false;
			}
		}

		private SelectorBarItem? FindSelectedItem()
			=> Items.FirstOrDefault(item => ReferenceEquals(item.Tag, SelectedValue));

		private void OnSelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
		{
			if (!_isSynchronizingSelection)
				SelectedValue = sender.SelectedItem?.Tag as NavigationBarItem;
		}
	}
}
