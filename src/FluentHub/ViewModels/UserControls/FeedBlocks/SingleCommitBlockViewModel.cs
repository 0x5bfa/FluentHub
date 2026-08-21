using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;

namespace FluentHub.ViewModels.UserControls.FeedBlocks
{
	public class SingleCommitBlockViewModel : ObservableObject
	{
		private PushActivityDetails _details = default!;
		public PushActivityDetails Details { get => _details; set => SetProperty(ref _details, value); }
	}
}
