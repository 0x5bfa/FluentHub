namespace FluentHub.App.ViewModels.UserControls.BlockButtons
{
	public class ProjectBlockButtonViewModel : ObservableObject
	{
		private Project _item;
		public Project Item { get => _item; set => SetProperty(ref _item, value); }
	}
}
