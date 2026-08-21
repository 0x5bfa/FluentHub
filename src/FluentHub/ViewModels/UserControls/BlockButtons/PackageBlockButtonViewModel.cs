using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;
using FluentHub.Core.Contracts;

namespace FluentHub.ViewModels.UserControls.BlockButtons
{
	public class PackageBlockButtonViewModel : ObservableObject
	{
		private Package _item = default!;
		public Package Item { get => _item; set => SetProperty(ref _item, value); }
	}
}
