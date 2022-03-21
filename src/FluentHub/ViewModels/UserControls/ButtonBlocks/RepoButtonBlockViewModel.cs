using Humanizer;
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
        private RepositoryBlockItem item;
        public RepositoryBlockItem Item { get => item; set => SetProperty(ref item, value); }

        private Brush primaryLangColor;
        public Brush PrimaryLangColor { get => primaryLangColor; set => SetProperty(ref primaryLangColor, value); }

        private bool displayDetails;
        public bool DisplayDetails { get => displayDetails; set => SetProperty(ref displayDetails, value); }

        private bool displayStarButton;
        public bool DisplayStarButton { get => displayStarButton; set => SetProperty(ref displayStarButton, value); }

        private string updatedAtHumanized;
        public string UpdatedAtHumanized { get => updatedAtHumanized; set => SetProperty(ref updatedAtHumanized, value); }

        public void GetColorBrush()
        {
            if (Item?.PrimaryLangColor != null)
            {
                PrimaryLangColor = ColorHelpers.HexCodeToSolidColorBrush(Item.PrimaryLangColor);
            }

            UpdatedAtHumanized = Item?.UpdatedAt.Humanize();
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
