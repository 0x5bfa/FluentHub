using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.Repositories.Codes
{
    public class CodePageViewModel : ObservableObject
    {
        private bool displayTreeView = false;
        public bool DisplayTreeView { get => displayTreeView; set => SetProperty(ref displayTreeView, value); }
    }
}
