// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Data;
using FluentHub.Octokit.Contracts;
using FluentHub.Services;

namespace FluentHub.Converters
{
	public partial class IssueCommentToIssueCommentBlockViewModelConverter : IValueConverter
	{
		public object Convert(object? value, Type targetType, object? parameter, string language)
		{
			var issueCommentBlockViewModel = Ioc.Default.GetRequiredService<ViewModels.UserControls.IssueCommentBlockViewModel>();
			if (value is IssueComment issueComment)
				issueCommentBlockViewModel.IssueComment = issueComment;

			return issueCommentBlockViewModel;
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, string language)
			=> throw new NotImplementedException();
	}
}
