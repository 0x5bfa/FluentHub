using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.UserControls.Labels
{
    public sealed partial class StateLabel : UserControl
    {
        #region StatusProperty
        public static readonly DependencyProperty StatusProperty
            = DependencyProperty.Register(
                  nameof(Status),
                  typeof(Status),
                  typeof(StateLabel),
                  new PropertyMetadata(null)
                );

        public Status Status
        {
            get => (Status)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }
        #endregion

        public StateLabel()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            switch (Status)
            {
                case Status.IssueOpened:
                    StateLabelBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x34, 0x7d, 0x39));
                    StateLabelFont.Glyph = "\uE9EA";
                    StateLabelTextBlock.Text = "Open";
                    break;
                case Status.IssueClosed:
                    StateLabelBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xc9, 0x3c, 0x37));
                    StateLabelFont.Glyph = "\uE9E6";
                    StateLabelTextBlock.Text = "Closed";
                    break;
                case Status.IssueDraft:
                    StateLabelBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x63, 0x6e, 0x7b));
                    StateLabelFont.Glyph = "\uE9E8";
                    StateLabelTextBlock.Text = "Draft";
                    break;

                case Status.PullOpened:
                    StateLabelBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x34, 0x7d, 0x39));
                    StateLabelFont.Glyph = "\uE9BF";
                    StateLabelTextBlock.Text = "Open";
                    break;
                case Status.PullClosed:
                    StateLabelBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xc9, 0x3c, 0x37));
                    StateLabelFont.Glyph = "\uE9BF";
                    StateLabelTextBlock.Text = "Closed";
                    break;
                case Status.PullMerged:
                    StateLabelBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x82, 0x56, 0xd0));
                    StateLabelFont.Glyph = "\uE9BD";
                    StateLabelTextBlock.Text = "Marged";
                    break;
                case Status.PullDraft:
                    StateLabelBorder.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x63, 0x6e, 0x7b));
                    StateLabelFont.Glyph = "\uE9BF";
                    StateLabelTextBlock.Text = "Draft";
                    break;
            }
        }
    }

    public enum Status
    {
        IssueOpened,
        IssueClosed,
        IssueDraft,
        PullOpened,
        PullClosed,
        PullMerged,
        PullDraft,
    }
}
