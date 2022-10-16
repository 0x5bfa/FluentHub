using FluentHub.App.Services;
using FluentHub.App.ViewModels.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.Dialogs
{
    public sealed partial class ExceptionStackTraceDialog : ContentDialog
    {
        private ObservableContext Context { get; }

        public ExceptionStackTraceDialog(Exception ex)
        {
            Context = new() { TaskException = ex };

            InitializeComponent();
        }

        private void OnContentDialogPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
            => Hide();

        private class ObservableContext : ObservableObject
        {
            public ObservableContext()
            {
                TaskException = new();
            }

            private Exception _taskException;
            public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }
        }
    }
}
