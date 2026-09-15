// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;

namespace FluentHub.Controls;

public sealed partial class LoadErrorDisplay : UserControl
{
	public static readonly DependencyProperty ErrorMessageProperty =
		DependencyProperty.Register(
			nameof(ErrorMessage),
			typeof(string),
			typeof(LoadErrorDisplay),
			new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty RetryCommandProperty =
		DependencyProperty.Register(
			nameof(RetryCommand),
			typeof(ICommand),
			typeof(LoadErrorDisplay),
			new PropertyMetadata(null));

	public LoadErrorDisplay()
	{
		InitializeComponent();
	}

	public string? ErrorMessage
	{
		get => (string?)GetValue(ErrorMessageProperty);
		set => SetValue(ErrorMessageProperty, value);
	}

	public ICommand? RetryCommand
	{
		get => (ICommand?)GetValue(RetryCommandProperty);
		set => SetValue(RetryCommandProperty, value);
	}
}
