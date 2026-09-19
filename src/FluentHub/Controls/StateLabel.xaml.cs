// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls;

public enum StateLabelSize
{
	Small,
	Medium,
}

/// <summary>
/// Displays an issue or pull request state using the Primer StateLabel pattern.
/// </summary>
public sealed partial class StateLabel : UserControl
{
	public StateLabel()
	{
		InitializeComponent();
		UpdateVisualStates();
	}

	/// <summary>
	/// Gets or sets the Primer status name, such as <c>issueOpened</c> or <c>pullMerged</c>.
	/// </summary>
	[GeneratedDependencyProperty(DefaultValue = "unavailable")]
	public partial string Status { get; set; }

	/// <summary>
	/// Gets or sets the label size.
	/// </summary>
	[GeneratedDependencyProperty(DefaultValue = StateLabelSize.Medium)]
	public partial StateLabelSize Size { get; set; }

	/// <summary>
	/// Gets or sets the localized state text.
	/// </summary>
	[GeneratedDependencyProperty(DefaultValue = "")]
	public partial string Text { get; set; }

	partial void OnStatusPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		UpdateStatusVisualState();
	}

	partial void OnSizePropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		UpdateSizeVisualState();
	}

	private void UpdateVisualStates()
	{
		UpdateStatusVisualState();
		UpdateSizeVisualState();
	}

	private void UpdateStatusVisualState()
	{
		VisualStateManager.GoToState(this, GetStatusVisualStateName(Status), false);
	}

	private void UpdateSizeVisualState()
	{
		VisualStateManager.GoToState(this, Size == StateLabelSize.Small ? "Small" : "Medium", false);
	}

	private static string GetStatusVisualStateName(string? status)
	{
		return status switch
		{
			"issueOpened" => "IssueOpened",
			"pullOpened" => "PullOpened",
			"issueClosed" => "IssueClosed",
			"issueClosedNotPlanned" => "IssueClosedNotPlanned",
			"pullClosed" => "PullClosed",
			"pullMerged" => "PullMerged",
			"draft" => "Draft",
			"issueDraft" => "IssueDraft",
			"pullQueued" => "PullQueued",
			"open" => "Open",
			"closed" => "Closed",
			_ => "Unavailable",
		};
	}
}
