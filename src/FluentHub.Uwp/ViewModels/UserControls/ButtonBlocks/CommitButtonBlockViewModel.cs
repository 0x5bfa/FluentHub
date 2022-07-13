using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks
{
    public class CommitButtonBlockViewModel : ObservableObject
    {
        private Commit _commitItem;
        public Commit CommitItem { get => _commitItem; set => SetProperty(ref _commitItem, value); }

        private Frame _targetFrame;
        public Frame TargetFrame { get => _targetFrame; set => SetProperty(ref _targetFrame, value); }
    }
}
