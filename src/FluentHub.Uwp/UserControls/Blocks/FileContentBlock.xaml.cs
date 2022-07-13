using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Uwp.UserControls.Blocks
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
            set
            {
                SetValue(ContextViewModelProperty, value);

                if (ContextViewModel != null)
                {
                    ViewModel.ContextViewModel = ContextViewModel;
                    ViewModel.LoadContentsAsync(ColorCodeBlock);
                }
            }
        }
        #endregion

        public FileContentBlock()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<FileContentBlockViewModel>();
        }

        public FileContentBlockViewModel ViewModel { get; }

        private void OnFileContentBlockLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
