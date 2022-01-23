using FluentHub.Helpers;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentHub.Models.Items
{
    public class RepoListItem
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public string Tag { get; private set; }
        public ImageSource Avator { get; private set; }
        public string Owner { get; private set; }

        public int StarCount { get; private set; }
        public string Language { get; private set; }

        public DateTimeOffset UpdatedAt { get; private set; }

        public CornerRadius Radius { get; private set; } = new CornerRadius(12);

        public SolidColorBrush LanguageColor { get; private set; }
            = new SolidColorBrush(Windows.UI.Color.FromArgb(0x0, 0x0, 0x0, 0x0));

        public RepoListItem(Repository item)
        {
            Avator = new BitmapImage(new Uri(item.Owner.AvatarUrl));

            _ = (item.Owner.Type == AccountType.Organization)
                ? (Radius = new CornerRadius(6)) : (Radius = new CornerRadius(12));

            Owner = item.Owner.Login;
            Name = item.Name;
            Description = item.Description;
            StarCount = item.StargazersCount;
            Language = item.Language;
            UpdatedAt = item.CreatedAt;

            var brush = LanguageColorHelpers.Get(item.Language);

            if (brush != null)
            {
                LanguageColor = brush;
            }

            Tag = item.Id.ToString();
        }
    }
}
