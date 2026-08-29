using FluentHub.Core.Application.Models;
using FluentHub.Converters;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls
{
	public sealed partial class UserContributionGraph : UserControl
	{
		public static readonly DependencyProperty CalendarProperty =
			DependencyProperty.Register(
				nameof(Calendar),
				typeof(ContributionCalendar),
				typeof(UserContributionGraph),
				new PropertyMetadata(null, OnCalendarChanged));

		// ItemsRepeater requires WinRT-projectable vectors at the XAML ABI boundary.
		public static readonly DependencyProperty ItemsProperty =
			DependencyProperty.Register(
				nameof(Items),
				typeof(object),
				typeof(UserContributionGraph),
				new PropertyMetadata(null));

		public ContributionCalendar? Calendar
		{
			get => (ContributionCalendar?)GetValue(CalendarProperty);
			set => SetValue(CalendarProperty, value);
		}

		public object? Items
		{
			get => GetValue(ItemsProperty);
			set => SetValue(ItemsProperty, value);
		}

		public ObservableCollection<object> MonthItems { get; } = [];

		public UserContributionGraph()
		{
			InitializeComponent();

			Loaded += OnLoaded;
			ActualThemeChanged += OnActualThemeChanged;
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
			=> RefreshContributionDayColors();

		private void OnActualThemeChanged(FrameworkElement sender, object args)
			=> RefreshContributionDayColors();

		private static void OnCalendarChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			if (sender is not UserContributionGraph graph)
				return;

			graph.MonthItems.Clear();
			if (args.NewValue is not ContributionCalendar calendar)
				return;

			foreach (var month in calendar.Months)
				graph.MonthItems.Add(month);
		}

		private void RefreshContributionDayColors()
		{
			if (Resources["ContributionLevelToBrushConverter"] is not ContributionLevelToBrushConverter converter)
				return;

			converter.IsLightTheme = ActualTheme == ElementTheme.Light;
			ContributionDaysRepeater.ItemsSource = null;
			ContributionDaysRepeater.ItemsSource = Items;
		}
	}
}
