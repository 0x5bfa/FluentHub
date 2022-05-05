using FluentHub.ViewModels.UserControls.Labels;
using FluentHub.Octokit.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;

namespace FluentHub.ViewModels.UserControls.ButtonBlocks
{
    public class IssueButtonBlockViewModel : INotifyPropertyChanged
    {
        public IssueButtonBlockViewModel()
        {
            _labelViewModels = new();
            LabelViewModels = new(_labelViewModels);

            CommentCountLabel = new()
            {
                Color = "#36000000",
            };
        }

        private Issue _issueItem;
        public Issue IssueItem { get => _issueItem; set => SetProperty(ref _issueItem, value); }

        private readonly ObservableCollection<LabelControlViewModel> _labelViewModels;
        public ReadOnlyObservableCollection<LabelControlViewModel> LabelViewModels { get; }

        private LabelControlViewModel _commentCountLabel;
        public LabelControlViewModel CommentCountLabel { get => _commentCountLabel; set => SetProperty(ref _commentCountLabel, value); }

        public void LoadContents()
        {
            CommentCountLabel.Name = IssueItem.CommentCount.ToString();

            _labelViewModels.Clear();
            foreach (var label in IssueItem.Labels)
            {
                LabelControlViewModel viewModel = new()
                {
                    Name = label.Name,
                    Color = label.Color,
                };

                _labelViewModels.Add(viewModel);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
