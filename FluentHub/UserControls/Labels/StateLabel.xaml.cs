using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FluentHub.Helpers;

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
                case Status.IssueOpened: // #1a7f37 [github light color bind theme]
                    LabelBackground.Background = ColorHelpers.HexCodeToSolidColorBrush("#16A34A");
                    StateLabelTextBlock.Foreground = ColorHelpers.HexCodeToSolidColorBrush("#A5D6A7");
                    StateLabelFont.Glyph = "\uE9EA";
                    StateLabelTextBlock.Text = "Open";
                    break;
                case Status.IssueClosed: // #8250df [github light color bind theme]
                    LabelBackground.Background = ColorHelpers.HexCodeToSolidColorBrush("#9333EA");
                    StateLabelTextBlock.Foreground = ColorHelpers.HexCodeToSolidColorBrush("#CE93D8");
                    StateLabelFont.Glyph = "\uE9E6";
                    StateLabelTextBlock.Text = "Closed";
                    break;
                case Status.IssueDraft: // #57606a [github light color bind theme]
                    LabelBackground.Background = ColorHelpers.HexCodeToSolidColorBrush("#52525B");
                    StateLabelTextBlock.Foreground = ColorHelpers.HexCodeToSolidColorBrush("#EEEEEE");
                    StateLabelFont.Glyph = "\uE9E8";
                    StateLabelTextBlock.Text = "Draft";
                    break;

                case Status.PullOpened: // #1a7f37 [github light color bind theme]
                    LabelBackground.Background = ColorHelpers.HexCodeToSolidColorBrush("#16A34A");
                    StateLabelTextBlock.Foreground = ColorHelpers.HexCodeToSolidColorBrush("#A5D6A7");
                    StateLabelFont.Glyph = "\uE9BF";
                    StateLabelTextBlock.Text = "Open";
                    break;
                case Status.PullClosed: // #cf222e [github light color bind theme]
                    LabelBackground.Background = ColorHelpers.HexCodeToSolidColorBrush("#DC2626");
                    StateLabelTextBlock.Foreground = ColorHelpers.HexCodeToSolidColorBrush("#EF9A9A");
                    StateLabelFont.Glyph = "\uE9C1";
                    StateLabelTextBlock.Text = "Closed";
                    break;
                case Status.PullMerged: // #8250df [github light color bind theme]
                    LabelBackground.Background = ColorHelpers.HexCodeToSolidColorBrush("#9333EA");
                    StateLabelTextBlock.Foreground = ColorHelpers.HexCodeToSolidColorBrush("#CE93D8");
                    StateLabelFont.Glyph = "\uE9BD";
                    StateLabelTextBlock.Text = "Marged";
                    break;
                case Status.PullDraft: // #57606a [github light color bind theme]
                    LabelBackground.Background = ColorHelpers.HexCodeToSolidColorBrush("#52525B");
                    StateLabelTextBlock.Foreground = ColorHelpers.HexCodeToSolidColorBrush("#EEEEEE");
                    StateLabelFont.Glyph = "\uE9C3";
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
