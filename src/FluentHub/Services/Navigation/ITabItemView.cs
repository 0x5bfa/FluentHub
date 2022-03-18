using System;
using System.ComponentModel;

namespace FluentHub.Services.Navigation
{
    public interface ITabItemView : INotifyPropertyChanged
    {
        Guid Guid { get; set; }
        string Header { get; set; }

    }
}
