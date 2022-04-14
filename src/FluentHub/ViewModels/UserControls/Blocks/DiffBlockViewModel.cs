using FluentHub.Octokit.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class DiffBlockViewModel : INotifyPropertyChanged
    {
        public DiffBlockViewModel()
        {
            _changedLineBackgroundType = new();

            ChangedLineBackgroundType = new(_changedLineBackgroundType);
        }

        private ChangedFile _changedFile;
        public ChangedFile ChangedFile { get => _changedFile; set => SetProperty(ref _changedFile, value); }

        private string _oldLineText;
        public string OldLineText { get => _oldLineText; set => SetProperty(ref _oldLineText, value); }

        private string _newLineText;
        public string NewLineText { get => _newLineText; set => SetProperty(ref _newLineText, value); }

        private ObservableCollection<int> _changedLineBackgroundType;
        public ObservableCollection<int> ChangedLineBackgroundType { get; }

        public void ParseDiffPatchString()
        {
            // TODO: FluentHub original perser here
            var lines = ChangedFile.Patch.Split("\n");

            // Display two line number column for added and deleted line
            for (int index = 0, olds = 0, news = 0; index < lines.Count(); index++)
            {
                // Pathch head line, like "@@ -5,7 +5,7 @@"
                if (lines[index][0] == '@')
                {
                    OldLineText += $"\n";
                    NewLineText += $"\n";

                    ChangedLineBackgroundType.Add(0);
                }
                // Added line
                else if (lines[index][0] == '+')
                {
                    OldLineText += $"\n";
                    NewLineText += $"{news}\n";

                    news++;

                    ChangedLineBackgroundType.Add(1);
                }
                // Deleted line
                else if (lines[index][0] == '-')
                {
                    OldLineText += $"{olds}\n";
                    NewLineText += $"\n";

                    olds++;

                    ChangedLineBackgroundType.Add(2);
                }
                // No changed line
                else
                {
                    OldLineText += $"{olds}\n";
                    NewLineText += $"{news}\n";
                    olds++;
                    news++;

                    ChangedLineBackgroundType.Add(3);
                }
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
