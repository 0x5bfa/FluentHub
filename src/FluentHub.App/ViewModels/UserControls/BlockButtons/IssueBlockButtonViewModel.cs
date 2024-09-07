namespace FluentHub.App.ViewModels.UserControls.BlockButtons
{
	public class IssueBlockButtonViewModel : ObservableObject
	{
		public IssueBlockButtonViewModel()
		{
		}

		#region Fields and Properties
		private Issue _issueItem;
		public Issue IssueItem { get => _issueItem; set => SetProperty(ref _issueItem, value); }

		private bool _compactMode;
		public bool CompactMode { get => _compactMode; set => SetProperty(ref _compactMode, value); }
		#endregion

		public void LoadContents()
		{
		}
	}
}
