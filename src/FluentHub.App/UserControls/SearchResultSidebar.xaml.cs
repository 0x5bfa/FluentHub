using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls
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

		private void OnSearchNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			var navigation = Ioc.Default.GetRequiredService<INavigationService>();
			var page = typeof(Views.Searches.RepositoriesPage);
			FrameNavigationParameter param = new()
			{
				Parameters = SearchTerm
			};

			switch (args.InvokedItemContainer.Tag.ToString().ToLower())
			{
				default:
				case "repositories":
					page = typeof(Views.Searches.RepositoriesPage);
					break;
				case "code":
					page = typeof(Views.Searches.CodePage);
					break;
				case "issues":
					page = typeof(Views.Searches.IssuesPage);
					break;
				case "users":
					page = typeof(Views.Searches.UsersPage);
					break;
			}

			navigation.Navigate(page, param);
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
