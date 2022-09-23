using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks
{
    public class PackageButtonBlockViewModel : ObservableObject
    {
        private Package _item;
        public Package Item { get => _item; set => SetProperty(ref _item, value); }
    }
}
