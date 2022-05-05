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

namespace FluentHub.ViewModels.UserControls.ButtonBlocks
{
    public class PullButtonBlockViewModel : INotifyPropertyChanged
    {
        public PullButtonBlockViewModel()
        {
            _labelViewModels = new();
            LabelViewModels = new(_labelViewModels);

            CommentCountLabel = new()
            {
                Color = "#36000000",
            };

            ReviewStateLabel = new()
            {
                Name = "Reviews",
                Color = "#36000000",
            };

            StatusStateLabel = new()
            {
                Name = "Status",
                Color = "#36000000",
            };
        }

        private PullRequest _pullItem;
        public PullRequest PullItem { get => _pullItem; set => SetProperty(ref _pullItem, value); }

        private readonly ObservableCollection<LabelControlViewModel> _labelViewModels;
        public ReadOnlyObservableCollection<LabelControlViewModel> LabelViewModels { get; }

        private LabelControlViewModel _commentCountLabel;
        public LabelControlViewModel CommentCountLabel { get => _commentCountLabel; set => SetProperty(ref _commentCountLabel, value); }

        private LabelControlViewModel _reviewStateLabel;
        public LabelControlViewModel ReviewStateLabel { get => _reviewStateLabel; set => SetProperty(ref _reviewStateLabel, value); }

        private LabelControlViewModel _statusStateLabel;
        public LabelControlViewModel StatusStateLabel { get => _statusStateLabel; set => SetProperty(ref _statusStateLabel, value); }

        public void LoadContents()
        {
            CommentCountLabel.Name = PullItem.CommentCount.ToString();

            _labelViewModels.Clear();
            foreach (var label in _pullItem.Labels)
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
