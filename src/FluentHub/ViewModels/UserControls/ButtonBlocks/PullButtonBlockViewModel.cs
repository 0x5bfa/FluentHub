using Humanizer;
using FluentHub.OctokitEx.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserControls.ButtonBlocks
{
    public class PullButtonBlockViewModel : INotifyPropertyChanged
    {
        public PullOverviewItem PullItem { get; set; } = new();

        private string nameWithOwner;
        public string NameWithOwner { get => nameWithOwner; set => SetProperty(ref nameWithOwner, value); }

        private string updatedAtHumanized;
        public string UpdatedAtHumanized { get => updatedAtHumanized; set => SetProperty(ref updatedAtHumanized, value); }

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
