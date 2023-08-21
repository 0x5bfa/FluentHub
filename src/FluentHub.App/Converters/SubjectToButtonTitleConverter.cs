// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Data;

namespace FluentHub.App.Converters
{
	public class SubjectToButtonTitleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			string result = null;
			var type = value.GetType().Name;

			switch (type)
			{
				case "Issue":
					{
						Issue issue = value as Issue;

						if (issue.Repository == null)
							return $"(unknown) / (unknown) #{issue.Number}";

						result = $"{issue.Repository.Owner.Login} / {issue.Repository.Name} #{issue.Number}";
					}
					break;
				case "PullRequest":
					{
						PullRequest pullRequest = value as PullRequest;

						if (pullRequest.Repository == null)
							return $"(unknown) / (unknown) #{pullRequest.Number}";

						result = $"{pullRequest.Repository.Owner.Login} / {pullRequest.Repository.Name} #{pullRequest.Number}";
					}
					break;
				case "Discussion":
					{
						Discussion discussion = value as Discussion;

						if (discussion.Repository == null)
							return $"(unknown) / (unknown) #{discussion.Number}";

						result = $"{discussion.Repository.Owner.Login} / {discussion.Repository.Name} #{discussion.Number}";
					}
					break;
			}

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
			=> throw new NotImplementedException();
	}
}
