using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.ViewModels.UserControls.BlockButtons
{
	public class DiscussionBlockButtonViewModel : ObservableObject
	{
		private Discussion _item = default!;
		public Discussion Item { get => _item; set => SetProperty(ref _item, value); }
	}
}
