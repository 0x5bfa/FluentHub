using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;
using FluentHub.Core.Application.Models;

namespace FluentHub.ViewModels.Controls.BlockButtons
{
	public class UserBlockButtonViewModel : ObservableObject
	{
		private User _user = default!;
		public User User { get => _user; set => SetProperty(ref _user, value); }
	}
}
