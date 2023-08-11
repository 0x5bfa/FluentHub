// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Issues;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
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
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadRepositoryIssuePageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
		}
	}
}
