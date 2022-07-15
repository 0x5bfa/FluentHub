using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using Windows.UI.Xaml.Media;

namespace FluentHub.Uwp.ViewModels.UserControls.Labels
{
    public class LabelControlViewModel : ObservableObject
    {
        #region Fields and Properties
        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private string _color;
        public string Color { get => _color; set => SetProperty(ref _color, value); }

        private SolidColorBrush _colorBrush;
        public SolidColorBrush ColorBrush { get => _colorBrush; set => SetProperty(ref _colorBrush, value); }

        private bool outlineEnable;
        public bool OutlineEnable { get => outlineEnable; set => SetProperty(ref outlineEnable, value); }

        private string _variant;
        public string Variant { get => _variant; set => SetProperty(ref _variant, value); }

        private bool _large;
        public bool Large { get => _large; set => SetProperty(ref _large, value); }
        #endregion

        public void SetColorBrush()
        {
            ColorBrush = Helpers.ColorHelpers.HexCodeToSolidColorBrush(Color);

            //switch (Variant)
            //{
            //    default:
            //    case "default":
            //        break;
            //    case "primary":
            //        break;
            //    case "secondary":
            //        break;
            //    case "accent":
            //        break;
            //    case "success":
            //        break;
            //    case "attention":
            //        break;
            //    case "severe":
            //        break;
            //    case "danger":
            //        break;
            //    case "done":
            //        break;
            //    case "sponsors":
            //        break;
            //}
        }
    }
}
