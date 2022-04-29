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

        private SolidColorBrush _color;
        public SolidColorBrush Color { get => _color; set => SetProperty(ref _color, value); }

        public void LoadContents(string subjectType)
        {
            switch (subjectType)
            {
                case "IssueOpen":
                    Color = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#347D39");
                    Glyph = "\uE9EA";
                    Name = "Open";
                    break;
                case "IssueClosed":
                    Color = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#8256D0");
                    Glyph = "\uE9E6";
                    Name = "Closed";
                    break;
                case "PullOpen":
                    Color = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#347D39");
                    Glyph = "\uE9BF";
                    Name = "Open";
                    break;
                case "PullClosed":
                    Color = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#C93C37");
                    Glyph = "\uE9C1";
                    Name = "Closed";
                    break;
                case "PullMerged":
                    Color = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#8256D0");
                    Glyph = "\uE9BD";
                    Name = "Marged";
                    break;
                case "PullDraft":
                    Color = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#636E7B");
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
