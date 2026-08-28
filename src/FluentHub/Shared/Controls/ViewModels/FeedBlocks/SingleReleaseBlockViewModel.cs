using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;

namespace FluentHub.Shared.Controls.ViewModels.FeedBlocks
{
	public class SingleReleaseBlockViewModel : ObservableObject
	{
		private ReleaseActivityDetails _details = default!;
		public ReleaseActivityDetails Details { get => _details; set => SetProperty(ref _details, value); }
	}
}
