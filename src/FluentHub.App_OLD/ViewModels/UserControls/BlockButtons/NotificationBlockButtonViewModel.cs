namespace FluentHub.App.ViewModels.UserControls.BlockButtons
{
	public class NotificationBlockButtonViewModel : ObservableObject
	{
		private Notification _item;
		public Notification Item { get => _item; set => SetProperty(ref _item, value); }
	}
}
