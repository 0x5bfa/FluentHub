// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

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
