using FluentHub.Services;
using FluentHub.ViewModels.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls
{
	public sealed partial class SearchResultSidebar : UserControl
	{
		#region propdp
		public static readonly DependencyProperty SelectedTagProperty =
			DependencyProperty.Register(
				nameof(SelectedTag),
				typeof(string),
				typeof(SearchResultSidebar),
				new PropertyMetadata(null));

		public string SelectedTag
		{
			get => (string)GetValue(SelectedTagProperty);
			set
			{
				SetValue(SelectedTagProperty, value);

				SelectItemByTag(value);
			}
		}

		public static readonly DependencyProperty SearchTermProperty =
			DependencyProperty.Register(
				nameof(SearchTerm),
				typeof(string),
				typeof(SearchResultSidebar),
				new PropertyMetadata(null));

		public string SearchTerm
		{
			get => (string)GetValue(SearchTermProperty);
			set
			{
				SetValue(SearchTermProperty, value);
			}
		}
		#endregion

		public SearchResultSidebar()
			=> InitializeComponent();

		private async void OnSearchNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			var navigation = Ioc.Default.GetRequiredService<INavigationService>();
			var kind = SearchKind.Repositories;

			if (args.InvokedItemContainer?.Tag is not { } tag)
				return;

			switch (tag.ToString()!.ToLowerInvariant())
			{
				default:
				case "repositories":
					kind = SearchKind.Repositories;
					break;
				case "code":
					kind = SearchKind.Code;
					break;
				case "issues":
					kind = SearchKind.Issues;
					break;
				case "users":
					kind = SearchKind.Users;
					break;
			}

			await navigation.NavigateAsync(new SearchRoute(kind, SearchTerm));
		}

		private void SelectItemByTag(string tag)
		{
			var defaultItem
				= SearchNavView
				.MenuItems
				.OfType<NavigationViewItem>()
				.FirstOrDefault();

			SearchNavView.SelectedItem
				= SearchNavView
				.MenuItems
				.OfType<NavigationViewItem>()
				.FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
				?? defaultItem;
		}
	}
}
