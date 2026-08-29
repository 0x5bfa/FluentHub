// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Data;
using FluentHub.Core.Application.Models;
using FluentHub.Services;

namespace FluentHub.Converters
{
	public partial class IssueCommentToIssueCommentBlockViewModelConverter : IValueConverter
	{
		public object Convert(object? value, Type targetType, object? parameter, string language)
		{
			var issueCommentBlockViewModel = Ioc.Default.GetRequiredService<ViewModels.Controls.IssueCommentBlockViewModel>();
			if (value is IssueComment issueComment)
				issueCommentBlockViewModel.IssueComment = issueComment;

			return issueCommentBlockViewModel;
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, string language)
			=> throw new NotImplementedException();
	}
}
