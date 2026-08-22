using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;
using FluentHub.Core.Contracts;

namespace FluentHub.ViewModels.UserControls.BlockButtons
{
	public class DiscussionBlockButtonViewModel : ObservableObject
	{
		private Discussion _item = default!;
		public Discussion Item { get => _item; set => SetProperty(ref _item, value); }
	}
}
