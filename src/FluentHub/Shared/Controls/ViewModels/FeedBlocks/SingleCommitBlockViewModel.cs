using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;

namespace FluentHub.Shared.Controls.ViewModels.FeedBlocks
{
	public class SingleCommitBlockViewModel : ObservableObject
	{
		private PushActivityDetails _details = default!;
		public PushActivityDetails Details { get => _details; set => SetProperty(ref _details, value); }
	}
}
