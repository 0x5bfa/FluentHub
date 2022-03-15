using FluentHub.ViewModels.Repositories;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.UserControls.Blocks
{
    public sealed partial class FileNavigationBlock : UserControl
    {
        #region RepositoryIdProperty
        public static readonly DependencyProperty RepositoryIdProperty
        = DependencyProperty.Register(
              nameof(RepositoryId),
              typeof(long),
              typeof(GitCloneFlyout),
              new PropertyMetadata(null)
            );

        public long RepositoryId
        {
            get => (long)GetValue(RepositoryIdProperty);
            set => SetValue(RepositoryIdProperty, value);
        }
        #endregion

        public static readonly DependencyProperty CommonRepoViewModelProperty =
            DependencyProperty.Register(
                nameof(CommonRepoViewModel),
                typeof(CommonRepoViewModel),
                typeof(FileNavigationBlock),
                new PropertyMetadata(0));

        public CommonRepoViewModel CommonRepoViewModel
        {
            get { return (CommonRepoViewModel)GetValue(CommonRepoViewModelProperty); }
            set { SetValue(CommonRepoViewModelProperty, value); }
        }

        public FileNavigationBlock() => InitializeComponent();
        public void SetState(UIElement target, string state)
        {
            if (target != null)
            {                
                muxc.AnimatedIcon.SetState(target, state);
            }
        }
        private void OnCloneButtonLoaded(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.AddHandler(PointerPressedEvent, new PointerEventHandler(OnCloneButtonPointerPressed), true);
            button.AddHandler(PointerReleasedEvent, new PointerEventHandler(OnCloneButtonPointerReleased), true);
        }
        private void OnCloneButtonPointerPressed(object sender, PointerRoutedEventArgs e) => SetState(sender as UIElement, "Pressed");

        private void OnCloneButtonPointerReleased(object sender, PointerRoutedEventArgs e) => SetState(sender as UIElement, "Normal");       
    }
}