using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.UserControls.Blocks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.UserControls.Blocks
{
    public sealed partial class FileContentBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ContextViewModelProperty =
            DependencyProperty.Register(
                nameof(ContextViewModel),
                typeof(RepoContextViewModel),
                typeof(FileContentBlock),
                new PropertyMetadata(null));

        public RepoContextViewModel ContextViewModel
        {
            get => (RepoContextViewModel)GetValue(ContextViewModelProperty);
            set => SetValue(ContextViewModelProperty, value);
        }
        #endregion

        public FileContentBlock()
        {
            this.InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<FileContentBlockViewModel>();
        }

        public FileContentBlockViewModel ViewModel { get; }

        private void OnFileContentBlockLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.ContextViewModel = ContextViewModel;

            var command = ViewModel.LoadBlobContentBlockCommand;
            if (command.CanExecute(ColorCodeBlock))
                command.Execute(ColorCodeBlock);
        }
    }
}
