namespace FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks.Search
{
    public class SearchIssueButtonBlockViewModel : ObservableObject
    {
        private IssueSearch _item;
        public IssueSearch Item { get => _item; set => SetProperty(ref _item, value); }
    }
}
