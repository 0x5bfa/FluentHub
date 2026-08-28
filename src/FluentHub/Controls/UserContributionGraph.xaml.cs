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
				new PropertyMetadata(null));

		public static readonly DependencyProperty ItemsProperty =
			DependencyProperty.Register(
				nameof(Items),
				typeof(IReadOnlyList<ContributionCalendarItem>),
				typeof(UserContributionGraph),
				new PropertyMetadata(null));

		public ContributionCalendar? Calendar
		{
			get => (ContributionCalendar?)GetValue(CalendarProperty);
			set => SetValue(CalendarProperty, value);
		}

		public IReadOnlyList<ContributionCalendarItem>? Items
		{
			get => (IReadOnlyList<ContributionCalendarItem>?)GetValue(ItemsProperty);
			set => SetValue(ItemsProperty, value);
		}

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
