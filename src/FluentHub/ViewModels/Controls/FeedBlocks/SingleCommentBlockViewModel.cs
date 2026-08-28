using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;

namespace FluentHub.ViewModels.Controls.FeedBlocks
{
	public class SingleCommentBlockViewModel : ObservableObject
	{
		private IssueCommentActivityDetails _details = default!;
		public IssueCommentActivityDetails Details { get => _details; set => SetProperty(ref _details, value); }
	}
}
