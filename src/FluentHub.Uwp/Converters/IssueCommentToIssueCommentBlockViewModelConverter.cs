using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FluentHub.Uwp.Converters
{
    public class IssueCommentToIssueCommentBlockViewModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IssueComment ic)
            {
                var issueCommentBlockViewModel = new ViewModels.UserControls.Blocks.IssueCommentBlockViewModel()
                {
                    IssueComment = ic,
                };

                return issueCommentBlockViewModel;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
