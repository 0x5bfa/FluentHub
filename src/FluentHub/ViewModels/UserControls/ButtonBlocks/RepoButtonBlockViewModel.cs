using FluentHub.Helpers;
using FluentHub.Octokit.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace FluentHub.ViewModels.UserControls.ButtonBlocks
{
    public class RepoButtonBlockViewModel : INotifyPropertyChanged
    {
        private Repository _item;
        public Repository Item { get => _item; set => SetProperty(ref _item, value); }

        private Brush _primaryLangColor;
        public Brush PrimaryLangColor { get => _primaryLangColor; set => SetProperty(ref _primaryLangColor, value); }

        private bool _displayDetails;
        public bool DisplayDetails { get => _displayDetails; set => SetProperty(ref _displayDetails, value); }

        private bool _displayStarButton;
        public bool DisplayStarButton { get => _displayStarButton; set => SetProperty(ref _displayStarButton, value); }

        public void GetColorBrush()
        {
            if (Item?.PrimaryLanguage != null)
            {
                PrimaryLangColor = ColorHelpers.HexCodeToSolidColorBrush(Item.PrimaryLanguage.Color);
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
