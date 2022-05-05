using FluentHub.Octokit.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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

        private string _fitstLetters;
        public string FirstLetters { get => _fitstLetters; set => SetProperty(ref _fitstLetters, value); }

        public string _patchRemovedfFirstLetters;
        public string PatchRemovedfFirstLetters { get => _patchRemovedfFirstLetters; set => SetProperty(ref _patchRemovedfFirstLetters, value); }

        private bool _blockIsExpanded;
        public bool BlockIsExpanded { get => _blockIsExpanded; set => SetProperty(ref _blockIsExpanded, value); }

        // 0: patch, 1: addition, 2: deletion, 3: no changes
        private readonly ObservableCollection<int> _changedLineBackgroundType;
        public ReadOnlyObservableCollection<int> ChangedLineBackgroundType { get; }

        public void Creanup()
        {
            if (string.IsNullOrEmpty(ChangedFile.Patch)) return;

            OldLineText = "";
            NewLineText = "";
            FirstLetters = "";
            PatchRemovedfFirstLetters = "";
            _changedLineBackgroundType.Clear();

            ParseDiffPatchString();

            OldLineText = OldLineText.TrimEnd('\n');
            NewLineText = NewLineText.TrimEnd('\n');
            FirstLetters = FirstLetters.TrimEnd('\n');
            PatchRemovedfFirstLetters = PatchRemovedfFirstLetters.TrimEnd('\n');
        }

        public void ParseDiffPatchString()
        {
            var lines = ChangedFile.Patch.Split("\n");
            bool isPatchLine = false;

            // Display two line number column for added and deleted line
            for (int index = 0, olds = 0, news = 0, oldBaseLine = 0, newBaseLine = 0; index < lines.Count(); index++)
            {
                if (Regex.IsMatch(lines[index], "^@@ -.* .+.* @@"))
                {
                    OldLineText += $"\n"; NewLineText += $"\n";

                    var array = Regex.Matches(lines[index], "[0-9]+").Cast<Match>().Select(x => int.Parse(x.Value)).ToArray();

                    oldBaseLine = array[0];
                    newBaseLine = array[2];
                    FirstLetters += "\n";
                    isPatchLine = true;

                    _changedLineBackgroundType.Add(0);
                }
                else if (lines[index][0] == '+')
                {
                    if (newBaseLine != 0)
                    {
                        news = newBaseLine;
                        newBaseLine = 0;
                    }
                    else news++;

                    OldLineText += $"\n";
                    NewLineText += $"{news}\n";
                    FirstLetters += "+\n";

                    _changedLineBackgroundType.Add(1);
                }
                else if (lines[index][0] == '-')
                {
                    if (oldBaseLine != 0)
                    {
                        olds = oldBaseLine;
                        oldBaseLine = 0;
                    }
                    else olds++;

                    OldLineText += $"{olds}\n";
                    NewLineText += $"\n";
                    FirstLetters += "-\n";

                    _changedLineBackgroundType.Add(2);
                }
                else
                {
                    if (oldBaseLine != 0)
                    {
                        olds = oldBaseLine;
                        oldBaseLine = 0;
                    }
                    else olds++;

                    if (newBaseLine != 0)
                    {
                        news = newBaseLine;
                        newBaseLine = 0;
                    }
                    else news++;

                    OldLineText += $"{olds}\n";
                    NewLineText += $"{news}\n";
                    FirstLetters += "\n";

                    _changedLineBackgroundType.Add(3);
                }

                if (isPatchLine != true) lines[index] = lines[index].Substring(1, lines[index].Length - 1);
                isPatchLine = false;
            }

            PatchRemovedfFirstLetters = string.Join("\n", lines);
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
