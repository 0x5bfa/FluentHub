// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.WinUI;
using System.Windows.Input;

namespace FluentHub.Controls;

public sealed partial class LoadErrorDisplay : UserControl
{
	public LoadErrorDisplay()
	{
		InitializeComponent();
	}

	[GeneratedDependencyProperty(DefaultValue = "")]
	public partial string? ErrorMessage { get; set; }

	[GeneratedDependencyProperty]
	public partial ICommand? RetryCommand { get; set; }
}
