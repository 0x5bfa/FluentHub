namespace FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks.Search
{
    public class SearchRepoButtonBlockViewModel : ObservableObject
    {
        private RepositorySearch _item;
        public RepositorySearch Item { get => _item; set => SetProperty(ref _item, value); }
    }
}
