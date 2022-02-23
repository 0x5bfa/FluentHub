using FluentHub.Views.Repositories;
using Humanizer;
using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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


namespace FluentHub.UserControls.ButtonBlocks
{
    public sealed partial class IssueButtonBlock : Windows.UI.Xaml.Controls.Page
    {
        public static readonly DependencyProperty RepositoryIdProperty
            = DependencyProperty.Register(
                  nameof(RepositoryId),
                  typeof(long),
                  typeof(IssueButtonBlock),
                  new PropertyMetadata(null)
                );

        public long RepositoryId
        {
            get => (long)GetValue(RepositoryIdProperty);
            set => SetValue(RepositoryIdProperty, value);
        }

        public static readonly DependencyProperty IssueIndexProperty
            = DependencyProperty.Register(
                  nameof(IssueIndex),
                  typeof(int),
                  typeof(IssueButtonBlock),
                  new PropertyMetadata(null)
                );

        public int IssueIndex
        {
            get => (int)GetValue(IssueIndexProperty);
            set => SetValue(IssueIndexProperty, value);
        }

        private ObservableCollection<LabelItem> _items = new ObservableCollection<LabelItem>();

        public IssueButtonBlock()
        {
            this.InitializeComponent();
        }

        private void IssueBlockButton_Click(object sender, RoutedEventArgs e)
        {
            string param = RepositoryId + "/" + IssueIndex;
            App.MainViewModel.RepoMainFrame.Navigate(typeof(IssuePage), param);
        }

        public SolidColorBrush GetSolidColorBrush(string hex)
        {
            var r = (byte)Convert.ToUInt32(hex.Substring(0, 2), 16);
            var g = (byte)Convert.ToUInt32(hex.Substring(2, 2), 16);
            var b = (byte)Convert.ToUInt32(hex.Substring(4, 2), 16);
            var brush = new SolidColorBrush(Color.FromArgb(0xFF, r, g, b));
            return brush;
        }

        private async void ItemsRepeater_Loaded(object sender, RoutedEventArgs e)
        {
            var issue = await App.Client.Issue.Get(RepositoryId, IssueIndex);

            if (issue.PullRequest != null)
            {
                var pr = await App.Client.PullRequest.Get(RepositoryId, IssueIndex);

                if (pr.Merged == true) // purple
                {
                    StatusFontGlyph.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x82, 0x50, 0xDF));
                    StatusFontGlyph.Glyph = "\uE9BD";
                }
                else if (pr.Draft == true) // gray
                {
                    StatusFontGlyph.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x57, 0x60, 0x6A));
                    StatusFontGlyph.Glyph = "\uE9C3";
                }
                else
                {
                    if (pr.State == ItemState.Open) // green
                    {
                        StatusFontGlyph.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1A, 0x7F, 0x37));
                        StatusFontGlyph.Glyph = "\uE9BF";
                    }
                    else if (pr.State == ItemState.Closed) // red
                    {
                        StatusFontGlyph.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xCF, 0x22, 0x2E));
                        StatusFontGlyph.Glyph = "\uE9C1";
                    }
                }
            }
            else
            {
                if (issue.State == ItemState.Open)
                {
                    StatusFontGlyph.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1A, 0x7F, 0x37));
                    StatusFontGlyph.Glyph = "\uE9EA";
                }
                else // purple
                {
                    StatusFontGlyph.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x82, 0x50, 0xDF));
                    StatusFontGlyph.Glyph = "\uE9E6";
                }

            }

            var repo = await App.Client.Repository.Get(RepositoryId);

            RepoNameWithOwnerAndNumber.Text = repo.Owner.Login + " / " + repo.Name + " #" + issue.Number;

            UpdatedAtHumanized.Text = issue.UpdatedAt.Humanize();

            TitleTextBlock.Text = issue.Title;

            foreach (var label in issue.Labels)
            {
                LabelItem labelItem = new LabelItem();

                var brush = GetSolidColorBrush(label.Color);

                labelItem.AccentColor = brush;

                labelItem.LabelText = label.Name;

                _items.Add(labelItem);
            }
        }
    }

    public class LabelItem
    {
        public string LabelText { get; set; }
        public Brush AccentColor { get; set; }
    }
}
