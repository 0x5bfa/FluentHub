namespace FluentHub.App.ViewModels.UserControls.BlockButtons
{
	public class OrgBlockButtonViewModel : ObservableObject
	{
		private Organization _orgItem;
		public Organization OrgItem { get => _orgItem; set => SetProperty(ref _orgItem, value); }
	}
}
