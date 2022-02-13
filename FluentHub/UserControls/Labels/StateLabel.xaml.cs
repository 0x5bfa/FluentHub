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
            set { SetValue(StatusProperty, value); SetContents(); }
        }
        #endregion

        public StateLabel()
        {
            this.InitializeComponent();
        }

        private void SetContents()
        {
            switch (Status)
            {
                case Status.IssueOpened: // 94e2a6
                    LabelBackground.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x94, 0xE2, 0xA6));
                    StateLabelFont.Glyph = "\uE9EA";
                    StateLabelTextBlock.Text = "Open";
                    break;
                case Status.IssueClosed: // b797f3
                    LabelBackground.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xB7, 0x97, 0xF3));
                    StateLabelFont.Glyph = "\uE9E6";
                    StateLabelTextBlock.Text = "Closed";
                    break;
                case Status.IssueDraft: // bdbdc7
                    LabelBackground.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xBD, 0xBD, 0xC7));
                    StateLabelFont.Glyph = "\uE9E8";
                    StateLabelTextBlock.Text = "Draft";
                    break;

                case Status.PullOpened: // 94e2a6
                    LabelBackground.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x94, 0xE2, 0xA6));
                    StateLabelFont.Glyph = "\uE9BF";
                    StateLabelTextBlock.Text = "Open";
                    break;
                case Status.PullClosed: // f47d88
                    LabelBackground.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF4, 0x7D, 0x88));
                    StateLabelFont.Glyph = "\uE9BF";
                    StateLabelTextBlock.Text = "Closed";
                    break;
                case Status.PullMerged: // b797f3
                    LabelBackground.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xB7, 0x97, 0xF3));
                    StateLabelFont.Glyph = "\uE9BD";
                    StateLabelTextBlock.Text = "Marged";
                    break;
                case Status.PullDraft: // bdbdc7
                    LabelBackground.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x63, 0x6e, 0x7b));
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
