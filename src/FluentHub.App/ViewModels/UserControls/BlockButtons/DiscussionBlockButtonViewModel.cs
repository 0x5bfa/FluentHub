using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Utils;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.App.ViewModels.UserControls.BlockButtons
{
	public class DiscussionBlockButtonViewModel : ObservableObject
	{
		private Discussion _item;
		public Discussion Item { get => _item; set => SetProperty(ref _item, value); }
	}
}
