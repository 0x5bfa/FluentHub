// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Data;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.App.Converters
{
	public partial class IssueCommentToIssueCommentBlockViewModelConverter : IValueConverter
	{
		public object Convert(object? value, Type targetType, object? parameter, string language)
		{
			if (value is not IssueComment ic)
				return new ViewModels.UserControls.IssueCommentBlockViewModel();

			var issueCommentBlockViewModel = new ViewModels.UserControls.IssueCommentBlockViewModel()
			{
				IssueComment = ic,
			};

			// Parse reactions nodes
			foreach (var reaction in (ic.Reactions?.Nodes ?? Enumerable.Empty<Reaction?>()).OfType<Reaction>())
			{
				_ = reaction.Content switch
				{
					ReactionContent.ThumbsUp => issueCommentBlockViewModel.ThumbsUpCount++,
					ReactionContent.ThumbsDown => issueCommentBlockViewModel.ThumbsDownCount++,
					ReactionContent.Laugh => issueCommentBlockViewModel.LaughCount++,
					ReactionContent.Hooray => issueCommentBlockViewModel.HoorayCount++,
					ReactionContent.Confused => issueCommentBlockViewModel.ConfusedCount++,
					ReactionContent.Heart => issueCommentBlockViewModel.HeartCount++,
					ReactionContent.Rocket => issueCommentBlockViewModel.RocketCount++,
					ReactionContent.Eyes => issueCommentBlockViewModel.EyesCount++,
					_ => 0,
				};
			}

			return issueCommentBlockViewModel;
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, string language)
			=> throw new NotImplementedException();
	}
}
