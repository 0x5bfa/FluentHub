// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Data;
using FluentHub.Core.Contracts;

namespace FluentHub.Converters
{
	public partial class SubjectToButtonTitleConverter : IValueConverter
	{
		public object Convert(object? value, Type targetType, object? parameter, string language)
		{
			switch (value)
			{
				case Issue issue:
					{
						if (issue.Repository == null)
							return $"(unknown) / (unknown) #{issue.Number}";

						return $"{issue.Repository.Owner.Login} / {issue.Repository.Name} #{issue.Number}";
					}
				case PullRequest pullRequest:
					{
						if (pullRequest.Repository == null)
							return $"(unknown) / (unknown) #{pullRequest.Number}";

						return $"{pullRequest.Repository.Owner.Login} / {pullRequest.Repository.Name} #{pullRequest.Number}";
					}
				case Discussion discussion:
					{
						if (discussion.Repository == null)
							return $"(unknown) / (unknown) #{discussion.Number}";

						return $"{discussion.Repository.Owner.Login} / {discussion.Repository.Name} #{discussion.Number}";
					}
				default:
					return string.Empty;
			}
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, string language)
			=> throw new NotImplementedException();
	}
}
