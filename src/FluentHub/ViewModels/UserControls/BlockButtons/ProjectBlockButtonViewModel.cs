using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;
using FluentHub.Core.Contracts;

namespace FluentHub.ViewModels.UserControls.BlockButtons
{
	public class ProjectBlockButtonViewModel : ObservableObject
	{
		private ProjectV2 _item = default!;
		public ProjectV2 Item { get => _item; set => SetProperty(ref _item, value); }
	}
}
