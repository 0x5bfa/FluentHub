using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.UserControls.Labels;
using FluentHub.Octokit.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.ViewModels.UserControls.ButtonBlocks
{
    public class CommitButtonBlockViewModel : INotifyPropertyChanged
    {
        private Commit _commitItem;
        public Commit CommitItem { get => _commitItem; set => SetProperty(ref _commitItem, value); }

        private Frame _targetFrame;
        public Frame TargetFrame { get => _targetFrame; set => SetProperty(ref _targetFrame, value); }

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
