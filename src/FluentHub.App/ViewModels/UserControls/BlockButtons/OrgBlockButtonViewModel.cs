using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Utils;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.App.ViewModels.UserControls.BlockButtons
{
	public class OrgBlockButtonViewModel : ObservableObject
	{
		private Organization _orgItem;
		public Organization OrgItem { get => _orgItem; set => SetProperty(ref _orgItem, value); }
	}
}
