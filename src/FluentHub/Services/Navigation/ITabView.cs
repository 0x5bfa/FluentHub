using System;
using System.Collections.Generic;

namespace FluentHub.Services.Navigation
{
    public interface ITabView
    {
        ITabItemView SelectedItem { get; }
        IList<ITabItemView> Items { get; }
        ITabItemView CreateTab(object paramenter, Type page, bool setAsSelected = true);
    }
}