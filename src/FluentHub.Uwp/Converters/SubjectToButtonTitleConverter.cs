using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace FluentHub.Uwp.Converters
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

                        result = $"{issue.Repository.Owner.Login} / {issue.Repository.Name} #{issue.Number}";
                    }
                    break;
                case "PullRequest":
                    {
                        PullRequest pullRequest = value as PullRequest;

                        result = $"{pullRequest.Repository.Owner.Login} / {pullRequest.Repository.Name} #{pullRequest.Number}";
                    }
                    break;
                case "Discussion":
                    {
                        Discussion discussion = value as Discussion;

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
