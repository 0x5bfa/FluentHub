using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks
{
    public class DiscussionButtonBlockViewModel : ObservableObject
    {
        private Discussion _item;
        public Discussion Item { get => _item; set => SetProperty(ref _item, value); }
    }
}
