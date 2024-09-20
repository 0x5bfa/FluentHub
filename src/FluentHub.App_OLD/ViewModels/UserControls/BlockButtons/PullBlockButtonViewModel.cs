namespace FluentHub.App.ViewModels.UserControls.BlockButtons
{
	public class PullBlockButtonViewModel : ObservableObject
	{
		public PullBlockButtonViewModel()
		{
		}

		#region Fields and Properties
		private PullRequest _pullItem;
		public PullRequest PullItem { get => _pullItem; set => SetProperty(ref _pullItem, value); }
		#endregion

		public void LoadContents()
		{
		}
	}
}
