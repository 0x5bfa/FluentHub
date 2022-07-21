using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using Windows.UI.Xaml.Media;

namespace FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks
{
    public class RepoButtonBlockViewModel : ObservableObject
    {
        #region Fields and Properties
        private Repository _item;
        public Repository Item { get => _item; set => SetProperty(ref _item, value); }

        private Brush _primaryLangColor;
        public Brush PrimaryLangColor { get => _primaryLangColor; set => SetProperty(ref _primaryLangColor, value); }

        private bool _displayDetails;
        public bool DisplayDetails { get => _displayDetails; set => SetProperty(ref _displayDetails, value); }

        private bool _displayStarButton;
        public bool DisplayStarButton { get => _displayStarButton; set => SetProperty(ref _displayStarButton, value); }
        #endregion

        public void GetColorBrush()
        {
            //if (Item?.PrimaryLanguage != null)
            //{
            //    PrimaryLangColor = ColorHelpers.HexCodeToSolidColorBrush(Item.PrimaryLanguage.Color);
            //}
        }
    }
}
