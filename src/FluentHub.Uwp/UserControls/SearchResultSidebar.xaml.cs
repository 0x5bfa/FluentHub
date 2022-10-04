using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls
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
        {
            InitializeComponent();
        }

        private void OnSearchNavViewItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            var navigation = App.Current.Services.GetRequiredService<INavigationService>();
            var page = typeof(Views.Searches.RepositoriesPage);
            Models.FrameNavigationArgs param = new()
            {
                Parameters = new() { SearchTerm }
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
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault();

            SearchNavView.SelectedItem
                = SearchNavView
                .MenuItems
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
                ?? defaultItem;
        }
    }
}
