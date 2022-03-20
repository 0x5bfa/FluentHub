using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace FluentHub.Utils
{
    public class NavigationHistory<T> : ObservableObject
    {
        #region constructor
        public NavigationHistory()
        {
            _items = new ObservableCollection<T>();
            Items = new ReadOnlyObservableCollection<T>(_items);
            _canGoBack = _canGoForward = false;
            _currentItem = default;
            _currentItemIndex = -1;
        }
        #endregion
#if DEBUG
        ~NavigationHistory() => System.Diagnostics.Debug.WriteLine("~NavigationHistory");
#endif

        #region fields
        private bool _canGoBack;
        private bool _canGoForward;
        private T _currentItem;
        private int _currentItemIndex;
        private ObservableCollection<T> _items;
        #endregion

        #region properties
        public T this[int index] => Items[index];
        public T CurrentItem { get => _currentItem; private set => SetProperty(ref _currentItem, value); }
        public int CurrentItemIndex
        {
            get => _currentItemIndex;
            set
            {
                if (value == -1)
                    CurrentItem = default;
                else if (value >= 0 && value <= _items.Count)
                    CurrentItem = _items[value];
                else
                    throw new ArgumentOutOfRangeException(nameof(value));

                _currentItemIndex = value;
                OnPropertyChanged(nameof(CurrentItemIndex));
                Update();
            }
        }
        public bool CanGoBack { get => _canGoBack; private set => SetProperty(ref _canGoBack, value); }
        public bool CanGoForward { get => _canGoForward; private set => SetProperty(ref _canGoForward, value); }
        public ReadOnlyObservableCollection<T> Items { get; }
        #endregion

        #region methods
        public bool GoBack()
        {
            if (CanGoBack)
            {
                CurrentItemIndex--;
                Update();
                return true;
            }
            return false;
        }

        public bool GoForward()
        {
            if (CanGoForward)
            {
                CurrentItemIndex++;
                Update();
                return true;
            }
            return false;
        }

        public void NavigateTo(T item)
        {
            _items.Add(item);
            CurrentItemIndex = _items.Count - 1;
            Update();
        }

        public void NavigateTo(T item, int index)
        {
            if (index >= 0 && index <= _items.Count) // valid
            {
                if (index == 0)
                    ClearHistory();
                while (index < _items.Count)
                    _items.RemoveAt(_items.Count - 1);
                NavigateTo(item);
            }
            else
                throw new ArgumentOutOfRangeException(nameof(index));
        }

        public void ClearHistory()
        {
            _items.Clear();
            CurrentItemIndex = -1;
            Update();
        }

        private void Update()
        {
            CanGoBack = CurrentItemIndex > 0;
            CanGoForward = CurrentItemIndex < _items.Count - 1;
        }
        #endregion
    }
}