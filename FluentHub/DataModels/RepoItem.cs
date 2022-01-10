using FluentHub.Helper;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentHub.DataModels
{
    public class RepoItem
    {
        public string Tag { get; private set; } = "[tag]";
        public ImageSource Avator { get; private set; } = new BitmapImage();
        public string Username { get; private set; } = "[username]";

        public string RepoName { get; private set; } = "[reponane]";
        public string Description { get; private set; } = "[description]";

        public int StarCount { get; private set; } = 0;
        public string Language { get; private set; } = "[language]";

        public DateTimeOffset UpdatedAt { get; private set; }

        public CornerRadius Radius { get; private set; } = new CornerRadius(12);

        public SolidColorBrush LanguageColor { get; private set; } = new SolidColorBrush(Windows.UI.Color.FromArgb(0x0, 0x0, 0x0, 0x0));

        public RepoItem(Repository item)
        {
            Avator = new BitmapImage(new Uri(item.Owner.AvatarUrl));

            if (item.Owner.Type == AccountType.Organization)
            {
                Radius = new CornerRadius(6);
            }

            Username = item.Owner.Login;
            RepoName = item.Name;
            Description = item.Description;
            StarCount = item.StargazersCount;
            Language = item.Language;
            UpdatedAt = item.CreatedAt;

            var brush = LanguageColors.Get(item.Language);
            LanguageColor = brush;
            Tag = item.Id.ToString();
        }
    }
}
