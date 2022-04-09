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
    public class LabelControlViewModel : INotifyPropertyChanged
    {
        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private SolidColorBrush _backgroundColorBrush;
        public SolidColorBrush BackgroundColorBrush { get => _backgroundColorBrush; set => SetProperty(ref _backgroundColorBrush, value); }

        private SolidColorBrush _foregroundColorBrush;
        public SolidColorBrush ForegroundColorBrush { get => _foregroundColorBrush; set => SetProperty(ref _foregroundColorBrush, value); }

        private bool outlineEnable;
        public bool OutlineEnable { get => outlineEnable; set => SetProperty(ref outlineEnable, value); }

        private string _variant;
        public string Variant { get => _variant; set => SetProperty(ref _variant, value); }

        private bool _large;
        public bool Large { get => _large; set => SetProperty(ref _large, value); }

        public void SetColorBrush()
        {
            // If a color has already been set, such as an Issue label,
            // that color is also set in the foreground. Variant is unavailable.
            if (ForegroundColorBrush == null && BackgroundColorBrush != null)
            {
                ForegroundColorBrush = BackgroundColorBrush;
                return;
            }

            switch (Variant)
            {
                default:
                case "default":
                    break;
                case "primary":
                    break;
                case "secondary":
                    break;
                case "accent":
                    break;
                case "success":
                    break;
                case "attention":
                    break;
                case "severe":
                    break;
                case "danger":
                    break;
                case "done":
                    break;
                case "sponsors":
                    break;
            }
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
