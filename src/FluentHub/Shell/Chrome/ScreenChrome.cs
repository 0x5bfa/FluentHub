// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Shell.Chrome;

public sealed class ScreenChrome : ObservableObject
{
	private string _header = "FluentHub";
	private string _description = "FluentHub";
	private IconSource? _icon;
	private string? _errorMessage;

	public string Header
	{
		get => _header;
		set => SetProperty(ref _header, value);
	}

	public string Description
	{
		get => _description;
		set => SetProperty(ref _description, value);
	}

	public IconSource? Icon
	{
		get => _icon;
		set => SetProperty(ref _icon, value);
	}

	public string? ErrorMessage
	{
		get => _errorMessage;
		private set
		{
			if (SetProperty(ref _errorMessage, value))
				OnPropertyChanged(nameof(HasError));
		}
	}

	public bool HasError
		=> !string.IsNullOrWhiteSpace(ErrorMessage);

	public void ShowError(string message)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(message);
		ErrorMessage = message;
	}

	public void ClearError()
		=> ErrorMessage = null;
}
