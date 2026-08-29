using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;

namespace FluentHub.ViewModels.Controls.FeedBlocks
{
	public class SingleReleaseBlockViewModel : ObservableObject
	{
		private ReleaseActivityDetails _details = default!;
		public ReleaseActivityDetails Details { get => _details; set => SetProperty(ref _details, value); }
	}
}
