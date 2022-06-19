using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.UserControls
{
    [ContentProperty(Name = nameof(SettingsActionableElement))]
    public sealed partial class SettingsBlockControl : UserControl
    {
        #region propdp
        public FrameworkElement SettingsActionableElement { get; set; }

        public static readonly DependencyProperty ExpandableContentProperty =
            DependencyProperty.Register(
                nameof(ExpandableContent),
                typeof(FrameworkElement),
                typeof(SettingsBlockControl),
                new PropertyMetadata(null)
                );

        public FrameworkElement ExpandableContent
        {
            get => (FrameworkElement)GetValue(ExpandableContentProperty);
            set => SetValue(ExpandableContentProperty, value);
        }

        public static readonly DependencyProperty AdditionalDescriptionContentProperty =
            DependencyProperty.Register(
                nameof(AdditionalDescriptionContent),
                typeof(FrameworkElement),
                typeof(SettingsBlockControl),
                new PropertyMetadata(null)
                );

        public FrameworkElement AdditionalDescriptionContent
        {
            get => (FrameworkElement)GetValue(AdditionalDescriptionContentProperty);
            set => SetValue(AdditionalDescriptionContentProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(SettingsBlockControl),
                new PropertyMetadata(null)
                );

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                nameof(Description),
                typeof(string),
                typeof(SettingsBlockControl),
                new PropertyMetadata(null)
                );

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                nameof(Icon),
                typeof(IconElement),
                typeof(SettingsBlockControl),
                new PropertyMetadata(null)
                );

        public IconElement Icon
        {
            get => (IconElement)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IsClickableProperty =
            DependencyProperty.Register(
                nameof(IsClickable),
                typeof(bool),
                typeof(SettingsBlockControl),
                new PropertyMetadata(false)
                );

        public bool IsClickable
        {
            get => (bool)GetValue(IsClickableProperty);
            set => SetValue(IsClickableProperty, value);
        }
        #endregion

        public event RoutedEventHandler Click;

        public SettingsBlockControl() => InitializeComponent();

        private void ActionableButton_Click(object sender, RoutedEventArgs e)
            => Click?.Invoke(this, e);

        private void Expander_Expanding(muxc.Expander sender, muxc.ExpanderExpandingEventArgs args)
            => Click?.Invoke(this, new RoutedEventArgs());

        private void Expander_Collapsed(muxc.Expander sender, muxc.ExpanderCollapsedEventArgs args)
            => Click?.Invoke(this, new RoutedEventArgs());
    }
}
