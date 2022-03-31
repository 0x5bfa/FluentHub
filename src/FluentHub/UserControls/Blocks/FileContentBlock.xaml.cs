using ColorCode;
using ColorCode.Common;
using ColorCode.Styling;
using ColorCode.UWP.Common;
using FluentHub.Helpers;
using FluentHub.ViewModels.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Storage;
using Windows.Storage.Pickers;
namespace FluentHub.UserControls.Blocks
{
    public sealed partial class FileContentBlock : UserControl
    {
        public static readonly DependencyProperty ContextViewModelProperty =
            DependencyProperty.Register(
                nameof(ContextViewModel),
                typeof(RepoContextViewModel),
                typeof(FileContentBlock),
                new PropertyMetadata(0));

        public RepoContextViewModel ContextViewModel
        {
            get { return (RepoContextViewModel)GetValue(ContextViewModelProperty); }
            set { SetValue(ContextViewModelProperty, value); }
        }

        public FileContentBlock()
        {
            this.InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.CommonRepoViewModel = ContextViewModel;
            await ViewModel.GetFileContent();
            Render();
        }

        private void Render()
        {
            ColorCodeBlock.Blocks.Clear();
            ColorCodeBlock.Visibility = Visibility.Collapsed;
            PlainCodeBlock.Visibility = Visibility.Collapsed;
            var formatter = new RichTextBlockFormatter(ThemeHelper.ActualTheme);
            var extension = Path.GetExtension(ViewModel.CommonRepoViewModel.Path.Remove(0, 1)).Remove(0,1);
            var fileType = FileTypeHelper.GetFileTypeStringId(extension);

            if (!string.IsNullOrEmpty(fileType))
            {
                ILanguage lang = Languages.FindById(fileType);
                formatter.FormatRichTextBlock(ViewModel.BlobContent, lang, ColorCodeBlock);
                ColorCodeBlock.Visibility = Visibility.Visible;
            }
            else
            {
                PlainCodeBlock.Text = ViewModel.BlobContent;
                PlainCodeBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
