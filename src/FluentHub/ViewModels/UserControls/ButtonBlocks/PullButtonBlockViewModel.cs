using Humanizer;
using FluentHub.OctokitEx.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using FluentHub.Helpers;

namespace FluentHub.ViewModels.UserControls.ButtonBlocks
{
    public class PullButtonBlockViewModel : INotifyPropertyChanged
    {
        public PullOverviewItem PullItem { get; set; } = new();

        private string nameWithOwner;
        public string NameWithOwner { get => nameWithOwner; set => SetProperty(ref nameWithOwner, value); }

        private string updatedAtHumanized;
        public string UpdatedAtHumanized { get => updatedAtHumanized; set => SetProperty(ref updatedAtHumanized, value); }

        private string stateGlyph;
        public string StateGlyph { get => stateGlyph; set => SetProperty(ref stateGlyph, value); }

        private Brush stateGlyphForeground;
        public Brush StateGlyphForeground { get => stateGlyphForeground; set => SetProperty(ref stateGlyphForeground, value); }

        public void SetStateContents()
        {
            if (PullItem.IsClosed)
            {
                StateGlyph = "\uE9E6";
                StateGlyphForeground = ColorHelpers.HexCodeToSolidColorBrush("#986EE2");
            }
            else if (PullItem.IsDraft)
            {
                StateGlyph = "\uE9EA";
                StateGlyphForeground = ColorHelpers.HexCodeToSolidColorBrush("#57AB5A");
            }
            else if (PullItem.IsMerged)
            {
                StateGlyph = "\uE9EA";
                StateGlyphForeground = ColorHelpers.HexCodeToSolidColorBrush("#57AB5A");
            }
            else
            {
                StateGlyph = "\uE9EA";
                StateGlyphForeground = ColorHelpers.HexCodeToSolidColorBrush("#57AB5A");
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
