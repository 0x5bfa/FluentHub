using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;
using FluentHub.Core.Application.Models;

namespace FluentHub.ViewModels.Controls.BlockButtons
{
	public class PullBlockButtonViewModel : ObservableObject
	{
		public PullBlockButtonViewModel()
		{
		}

		#region Fields and Properties
		private PullRequest _pullItem = default!;
		public PullRequest PullItem { get => _pullItem; set => SetProperty(ref _pullItem, value); }
		#endregion

		public void LoadContents()
		{
		}
	}
}
