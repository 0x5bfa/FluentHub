using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.ViewModels.UserControls.BlockButtons
{
	public class OrgBlockButtonViewModel : ObservableObject
	{
		private Organization _orgItem = default!;
		public Organization OrgItem { get => _orgItem; set => SetProperty(ref _orgItem, value); }
	}
}
