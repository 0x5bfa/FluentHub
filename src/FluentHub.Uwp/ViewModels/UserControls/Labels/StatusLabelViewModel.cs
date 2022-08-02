using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using Windows.UI.Xaml.Media;

namespace FluentHub.Uwp.ViewModels.UserControls.Labels
{
    public class StatusLabelViewModel : ObservableObject
    {
        #region Fields and Properties
        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private string _glyph;
        public string Glyph { get => _glyph; set => SetProperty(ref _glyph, value); }

        private SolidColorBrush _colorBackground;
        public SolidColorBrush ColorBackground { get => _colorBackground; set => SetProperty(ref _colorBackground, value); }

        private SolidColorBrush _colorForeground;
        public SolidColorBrush ColorForeground { get => _colorForeground; set => SetProperty(ref _colorForeground, value); }
        #endregion

        public void LoadContents(string subjectType)
        {
            switch (subjectType)
            {
                case "IssueOpen":
                    ColorBackground = ColorHelpers.HexCodeToSolidColorBrush("#347D38");
                    ColorForeground = ColorHelpers.HexCodeToSolidColorBrush("#3fb950");
                    Glyph = "\uE9EA";
                    Name = "Open";
                    break;
                case "IssueClosed":
                    ColorBackground = ColorHelpers.HexCodeToSolidColorBrush("#8256D0");
                    ColorForeground = ColorHelpers.HexCodeToSolidColorBrush("#bc8cff");
                    Glyph = "\uE9E6";
                    Name = "Closed";
                    break;
                case "PullOpen":
                    ColorBackground = ColorHelpers.HexCodeToSolidColorBrush("#347D39");
                    ColorForeground = ColorHelpers.HexCodeToSolidColorBrush("#57AB5A");
                    Glyph = "\uE9BF";
                    Name = "Open";
                    break;
                case "PullClosed":
                    ColorBackground = ColorHelpers.HexCodeToSolidColorBrush("#C93C37");
                    ColorForeground = ColorHelpers.HexCodeToSolidColorBrush("#ff7b72");
                    Glyph = "\uE9C1";
                    Name = "Closed";
                    break;
                case "PullMerged":
                    ColorBackground = ColorHelpers.HexCodeToSolidColorBrush("#8256D0");
                    ColorForeground = ColorHelpers.HexCodeToSolidColorBrush("#986EE2");
                    Glyph = "\uE9BD";
                    Name = "Merged";
                    break;
                case "PullDraft":
                    ColorBackground = ColorHelpers.HexCodeToSolidColorBrush("#768390");
                    ColorForeground = ColorHelpers.HexCodeToSolidColorBrush("#8b939e");
                    Glyph = "\uE9C3";
                    Name = "Draft";
                    break;
            }
        }
    }
}
