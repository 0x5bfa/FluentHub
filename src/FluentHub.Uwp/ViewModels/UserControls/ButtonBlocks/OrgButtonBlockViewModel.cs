using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks
{
    public class OrgButtonBlockViewModel : ObservableObject
    {
        public Organization OrgItem { get; set; } = new();
    }
}
