using FluentHub.App.Extensions;
using FluentHub.App.ViewModels.Repositories;
using FluentHub.App.ViewModels.UserControls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;

namespace FluentHub.App.UserControls
{
	public sealed partial class ReadmeContentBlock : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ContextViewModelProperty =
			DependencyProperty.Register(
				nameof(ContextViewModel),
				typeof(RepoContextViewModel),
				typeof(ReadmeContentBlock),
				null);

		public RepoContextViewModel ContextViewModel
		{
			get => (RepoContextViewModel)GetValue(ContextViewModelProperty);
			set
			{
				SetValue(ContextViewModelProperty, value);

				if (value != null)
				{
					ViewModel.ContextViewModel = value;
					ViewModel.LoadRepositoryReadmeAsync(ReadmeContentWebView2);
				}
			}
		}
		#endregion

		public ReadmeContentBlock()
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<ReadmeContentBlockViewModel>();
		}

		public ReadmeContentBlockViewModel ViewModel { get; }

		private async void OnReadmeContentWebView2NavigationCompleted(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
			=> await sender.HandleResize();

		private async void OnReadmeContentWebView2SizeChanged(object sender, SizeChangedEventArgs e)
			=> await ((WebView2)sender).HandleResize();

		private void OnUserControlUnloaded(object sender, RoutedEventArgs e)
		{
			// https://github.com/microsoft/microsoft-ui-xaml/issues/4752
			ReadmeContentWebView2.Close();
		}
	}
}
