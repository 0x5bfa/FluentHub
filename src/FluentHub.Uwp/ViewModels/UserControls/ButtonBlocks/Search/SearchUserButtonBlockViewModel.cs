namespace FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks.Search
{
    public class SearchUserButtonBlockViewModel : ObservableObject
    {
        private UserSearch _item;
        public UserSearch Item { get => _item; set => SetProperty(ref _item, value); }
    }
}
