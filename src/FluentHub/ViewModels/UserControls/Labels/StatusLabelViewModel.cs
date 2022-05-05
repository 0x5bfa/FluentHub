using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace FluentHub.ViewModels.UserControls.Labels
{
    public class StatusLabelViewModel : INotifyPropertyChanged
    {
        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private string _glyph;
        public string Glyph { get => _glyph; set => SetProperty(ref _glyph, value); }

        private SolidColorBrush _colorBackground;
        public SolidColorBrush ColorBackground { get => _colorBackground; set => SetProperty(ref _colorBackground, value); }

        private SolidColorBrush _colorForeground;
        public SolidColorBrush ColorForeground { get => _colorForeground; set => SetProperty(ref _colorForeground, value); }

        public void LoadContents(string subjectType)
        {
            switch (subjectType)
            {
                case "IssueOpen":
                    ColorBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#347D38");
                    ColorForeground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#57AB5A");
                    Glyph = "\uE9EA";
                    Name = "Open";
                    break;
                case "IssueClosed":
                    ColorBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#8256D0");
                    ColorForeground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#986EE2");
                    Glyph = "\uE9E6";
                    Name = "Closed";
                    break;
                case "PullOpen":
                    ColorBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#347D39");
                    ColorForeground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#57AB5A");
                    Glyph = "\uE9BF";
                    Name = "Open";
                    break;
                case "PullClosed":
                    ColorBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#C93C37");
                    ColorForeground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#E5534B");
                    Glyph = "\uE9C1";
                    Name = "Closed";
                    break;
                case "PullMerged":
                    ColorBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#8256D0");
                    ColorForeground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#986EE2");
                    Glyph = "\uE9BD";
                    Name = "Merged";
                    break;
                case "PullDraft":
                    ColorBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#768390");
                    ColorForeground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#ADBAC7");
                    Glyph = "\uE9C3";
                    Name = "Draft";
                    break;
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
