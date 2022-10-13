using FluentHub.App.Services;
using Microsoft.Extensions.DependencyInjection;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.Utils
{
    public class NavigationHistory<T> : ObservableObject
    {
        public NavigationHistory()
        {
            _items = new ObservableCollection<T>();
            Items = new ReadOnlyObservableCollection<T>(_items);

            _canGoBack = _canGoForward = false;
            _currentItem = default;
            _currentItemIndex = -1;
        }

#if DEBUG
        ~NavigationHistory()
            => System.Diagnostics.Debug.WriteLine("~NavigationHistory");
#endif

        #region Fields and Properties
        private bool _canGoBack;
        public bool CanGoBack { get => _canGoBack; private set => SetProperty(ref _canGoBack, value); }

        private bool _canGoForward;
        public bool CanGoForward { get => _canGoForward; private set => SetProperty(ref _canGoForward, value); }

        private T _currentItem;
        public T CurrentItem { get => _currentItem; private set => SetProperty(ref _currentItem, value); }

        private ObservableCollection<T> _items;

        private int _currentItemIndex;
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

        public T this[int index] => Items[index];
        public ReadOnlyObservableCollection<T> Items { get; }
        #endregion

        #region Methods
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
            // Valid
            if (index >= 0 && index <= _items.Count)
            {
                if (index == 0)
                {
                    ClearHistory();
                }
                while (index < _items.Count)
                {
                    _items.RemoveAt(_items.Count - 1);
                }

                NavigateTo(item);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
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

        public static void SetCurrentItem(string header, string description, string url, muxc.IconSource icon)
        {
            INavigationService navigationService;
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;

            currentItem.Header = header;
            currentItem.Description = description;
            currentItem.Url = url;
            currentItem.DisplayUrl = "";
            currentItem.Icon = icon;
        }
        #endregion
    }
}
