using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
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
        #endregion

        public SearchResultSidebar()
        {
            InitializeComponent();
        }

        private void OnSearchNavViewItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            var service = App.Current.Services.GetRequiredService<INavigationService>();
            Type page = typeof(Views.Searches.RepositoriesPage);
            Models.FrameNavigationArgs param = new()
            {
                // TODO: Add seach term here
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
