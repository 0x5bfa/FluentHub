using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.ViewModels.UserControls.BlockButtons
{
	public class UserBlockButtonViewModel : ObservableObject
	{
		private User _user = default!;
		public User User { get => _user; set => SetProperty(ref _user, value); }
	}
}
