using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks
{
    public class ProjectButtonBlockViewModel : ObservableObject
    {
        private Project _item;
        public Project Item { get => _item; set => SetProperty(ref _item, value); }
    }
}
