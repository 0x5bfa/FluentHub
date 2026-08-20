using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;

namespace FluentHub.ViewModels.UserControls.BlockButtons
{
	public class NotificationBlockButtonViewModel : ObservableObject
	{
		private Notification _item = default!;
		public Notification Item { get => _item; set => SetProperty(ref _item, value); }
	}
}
