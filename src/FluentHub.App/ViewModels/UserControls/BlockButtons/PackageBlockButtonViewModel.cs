using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Utils;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.App.ViewModels.UserControls.BlockButtons
{
	public class PackageBlockButtonViewModel : ObservableObject
	{
		private Package _item;
		public Package Item { get => _item; set => SetProperty(ref _item, value); }
	}
}
