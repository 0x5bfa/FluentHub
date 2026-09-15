// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.System;

namespace FluentHub.Views;

public sealed partial class OrganizationView : UserControl
{
	private readonly Action<UIElement, AppRoute> _navigate;

	public OrganizationView(
		IFluentHubGitHubClient gitHub,
		OrganizationRoute route,
		Action<UIElement, AppRoute> navigate)
	{
		ViewModel = new OrganizationViewModel(gitHub, route);
		_navigate = navigate ?? throw new ArgumentNullException(nameof(navigate));

		InitializeComponent();
		Loaded += OnOrganizationViewLoaded;
		Unloaded += OnOrganizationViewUnloaded;
	}

	public OrganizationViewModel ViewModel { get; }

	private async void OnOrganizationViewLoaded(object sender, RoutedEventArgs e)
		=> await ViewModel.LoadAsync();

	private void OnOrganizationViewUnloaded(object sender, RoutedEventArgs e)
		=> ViewModel.CancelLoading();

	private void OnRepositoriesClick(object sender, RoutedEventArgs e)
		=> _navigate(this, new OrganizationRoute(ViewModel.Login, OrganizationSection.Repositories));

	private void OnOverviewClick(object sender, RoutedEventArgs e)
		=> _navigate(this, new OrganizationRoute(ViewModel.Login));

	private async void OnWebsiteClick(object sender, RoutedEventArgs e)
	{
		if (Uri.TryCreate(ViewModel.Website, UriKind.Absolute, out var uri))
			await Launcher.LaunchUriAsync(uri);
	}
}
