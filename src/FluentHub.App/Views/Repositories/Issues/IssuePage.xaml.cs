// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Repositories.Issues;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Issues
{
	public sealed partial class IssuePage : LocatablePage
	{
		public IssueViewModel ViewModel;

		public IssuePage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Issues)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<IssueViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryIssuePageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadRepositoryIssuePageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
