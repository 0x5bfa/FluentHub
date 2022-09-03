using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Uwp.Dialogs
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
