// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.
// Adapted from the WinUI Gallery CopyButton control.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.ApplicationModel.DataTransfer;

namespace FluentHub.Shared.Controls.Views
{
	public sealed partial class CopyButton : Button
	{
		public static readonly DependencyProperty ClipboardTextProperty =
			DependencyProperty.Register(
				nameof(ClipboardText),
				typeof(string),
				typeof(CopyButton),
				new PropertyMetadata(string.Empty));

		public static readonly DependencyProperty CopiedMessageProperty =
			DependencyProperty.Register(
				nameof(CopiedMessage),
				typeof(string),
				typeof(CopyButton),
				new PropertyMetadata("Copied to clipboard"));

		public string ClipboardText
		{
			get => (string)GetValue(ClipboardTextProperty);
			set => SetValue(ClipboardTextProperty, value);
		}

		public string CopiedMessage
		{
			get => (string)GetValue(CopiedMessageProperty);
			set => SetValue(CopiedMessageProperty, value);
		}

		public CopyButton()
		{
			DefaultStyleKey = typeof(CopyButton);
		}

		protected override void OnApplyTemplate()
		{
			Click -= OnCopyButtonClick;
			base.OnApplyTemplate();
			Click += OnCopyButtonClick;
		}

		private void OnCopyButtonClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(ClipboardText))
				return;

			var dataPackage = new DataPackage();
			dataPackage.SetText(ClipboardText);
			Clipboard.SetContent(dataPackage);
			Clipboard.Flush();

			if (GetTemplateChild("CopyToClipboardSuccessAnimation") is Storyboard storyboard)
				storyboard.Begin();

			var peer = FrameworkElementAutomationPeer.FromElement(this)
				?? FrameworkElementAutomationPeer.CreatePeerForElement(this);
			peer?.RaiseNotificationEvent(
				AutomationNotificationKind.ActionCompleted,
				AutomationNotificationProcessing.ImportantMostRecent,
				CopiedMessage,
				"CopiedToClipboardActivityId");
		}
	}
}
