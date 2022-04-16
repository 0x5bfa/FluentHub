using FluentHub.ViewModels.UserControls.Blocks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class IssueEventBlockViewModel : INotifyPropertyChanged
    {
        private string _eventType;
        public string EventType { get => _eventType; set => SetProperty(ref _eventType, value); }

        private object _event;
        public object Event { get => _event; set => SetProperty(ref _event, value); }

        private IssueCommentBlockViewModel _commentBlockViewModel;
        public IssueCommentBlockViewModel CommentBlockViewModel { get => _commentBlockViewModel; set => SetProperty(ref _commentBlockViewModel, value); }

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
