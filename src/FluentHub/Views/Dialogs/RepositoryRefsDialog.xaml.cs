using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Dialogs
{
	public enum RepositoryReferenceKind
	{
		Branch,
		Tag,
	}

	public sealed partial class RepositoryRefsDialog : ContentDialog
	{
		private readonly IReadOnlyList<string> _branches;
		private readonly string _currentReference;
		private readonly IReadOnlyList<string> _tags;

		public RepositoryRefsDialog(
			IReadOnlyList<string> branches,
			IReadOnlyList<string> tags,
			string currentReference,
			RepositoryReferenceKind initialKind)
		{
			_branches = branches;
			_tags = tags;
			_currentReference = currentReference;
			SelectedKind = initialKind;
			FilteredReferences = new();

			InitializeComponent();

			ReferenceTypeSelector.SelectedItem = initialKind == RepositoryReferenceKind.Branch
				? BranchesSelectorItem
				: TagsSelectorItem;
			ApplyFilter();
		}

		public string BranchesHeader => $"Branches ({_branches.Count})";
		public ObservableCollection<string> FilteredReferences { get; }
		public string? SelectedReference => ReferencesList.SelectedItem as string;
		public RepositoryReferenceKind SelectedKind { get; private set; }
		public string TagsHeader => $"Tags ({_tags.Count})";

		private void OnReferenceTypeSelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
		{
			SelectedKind = sender.SelectedItem == TagsSelectorItem
				? RepositoryReferenceKind.Tag
				: RepositoryReferenceKind.Branch;
			ApplyFilter();
		}

		private void OnSearchTextChanged(object sender, TextChangedEventArgs args)
			=> ApplyFilter();

		private void OnReferenceSelectionChanged(object sender, SelectionChangedEventArgs args)
			=> IsPrimaryButtonEnabled = SelectedReference is not null;

		private void ApplyFilter()
		{
			var references = SelectedKind == RepositoryReferenceKind.Branch ? _branches : _tags;
			var filter = SearchBox.Text.Trim();
			var selectedReference = SelectedReference;

			FilteredReferences.Clear();
			foreach (var reference in references)
			{
				if (filter.Length == 0 || reference.Contains(filter, StringComparison.OrdinalIgnoreCase))
					FilteredReferences.Add(reference);
			}

			var referenceToSelect = selectedReference ?? _currentReference;
			ReferencesList.SelectedItem = FilteredReferences.FirstOrDefault(
				reference => reference.Equals(referenceToSelect, StringComparison.Ordinal));
			EmptyStateText.Visibility = FilteredReferences.Count == 0
				? Visibility.Visible
				: Visibility.Collapsed;
			IsPrimaryButtonEnabled = SelectedReference is not null;
		}
	}
}
