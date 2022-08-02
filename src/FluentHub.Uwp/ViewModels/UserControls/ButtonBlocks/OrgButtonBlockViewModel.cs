using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks
{
    public class OrgButtonBlockViewModel : ObservableObject
    {
        private Organization _orgItem;
        public Organization OrgItem { get => _orgItem; set => SetProperty(ref _orgItem, value); }
    }
}
