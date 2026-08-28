// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace FluentHub.Shared.Dialogs.Views
{
	public sealed partial class CreateForkDialog : ContentDialog
	{
		private readonly string _initialDescription;
		private bool _isReady;

		public CreateForkDialog(
			IReadOnlyList<ForkOwner> owners,
			string repositoryName,
			string? description,
			string defaultBranchName)
		{
			ArgumentNullException.ThrowIfNull(owners);
			Owners = new ObservableCollection<ForkOwner>(owners);
			_initialDescription = description ?? string.Empty;
			DefaultBranchDescription = string.IsNullOrWhiteSpace(defaultBranchName)
				? "Only the upstream repository's default branch will be copied."
				: $"Only the {defaultBranchName} branch will be copied.";

			InitializeComponent();

			RepositoryNameTextBox.Text = repositoryName;
			DescriptionTextBox.Text = _initialDescription;
			_isReady = true;
			UpdateDescriptionLength();
			UpdatePrimaryButtonState();
		}

		public bool DefaultBranchOnly
			=> DefaultBranchOnlyCheckBox.IsChecked is true;

		public string DefaultBranchDescription { get; }

		public string? Description
			=> string.Equals(DescriptionTextBox.Text, _initialDescription, StringComparison.Ordinal)
				? null
				: DescriptionTextBox.Text;

		public ObservableCollection<ForkOwner> Owners { get; }

		public string RepositoryName
			=> RepositoryNameTextBox.Text.Trim();

		public ForkOwner? SelectedOwner
			=> OwnerComboBox.SelectedItem as ForkOwner;

		private void OnDescriptionTextChanged(object sender, TextChangedEventArgs args)
		{
			if (!_isReady)
				return;

			UpdateDescriptionLength();
			UpdatePrimaryButtonState();
		}

		private void OnInputChanged(object sender, RoutedEventArgs args)
		{
			if (_isReady)
				UpdatePrimaryButtonState();
		}

		private void UpdateDescriptionLength()
			=> DescriptionLengthTextBlock.Text = $"{DescriptionTextBox.Text.Length} / {DescriptionTextBox.MaxLength}";

		private void UpdatePrimaryButtonState()
			=> IsPrimaryButtonEnabled =
				SelectedOwner is not null &&
				!string.IsNullOrWhiteSpace(RepositoryNameTextBox.Text);
	}
}
