using FluentHub.App.Utils;
using FluentHub.App.ViewModels.Repositories;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.ViewModels.UserControls
{
	public class ReadmeContentBlockViewModel : ObservableObject
	{
		public ReadmeContentBlockViewModel(IMessenger messenger = null, ILogger logger = null)
		{
			_messenger = messenger;
			_logger = logger;
		}

		#region Fields and Properties
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;

		private RepoContextViewModel contextViewModel;
		public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

		private bool _managedToLoadReadmeContents;
		public bool ManagedToLoadReadmeContents { get => _managedToLoadReadmeContents; set => SetProperty(ref _managedToLoadReadmeContents, value); }
		#endregion

		public async Task LoadRepositoryReadmeAsync(WebView2 webView2)
		{
			try
			{
				var settingTheme = Helpers.ThemeHelpers.RootTheme.ToString();
				var appTheme = App.Current.RequestedTheme.ToString().ToLower();

				if (settingTheme == "default")
					settingTheme = appTheme;

				MarkdownApiHandler queries = new();
				var html = await queries.GetHtmlAsync(
					ContextViewModel.Repository.Owner.Login,
					ContextViewModel.Repository.Name,
					ContextViewModel.BranchName,
					settingTheme
					);

				if (html == null)
					return;

				//// https://github.com/microsoft/microsoft-ui-xaml/issues/3714
				//await webView2.EnsureCoreWebView2Async();

				//// https://github.com/microsoft/microsoft-ui-xaml/issues/1967
				//// It is no longer the plan for WebView2 to support ms-appx-web:/// and ms-appx-data:///.
				//// Instead of using these proprietary protocols the SetVirtualHostNameToFolderMapping API is recommended.
				//var CoreWebView2 = webView2.CoreWebView2;
				//if (CoreWebView2 != null)
				//{
				//	CoreWebView2.SetVirtualHostNameToFolderMapping(
				//		"fluenthub.app", "Assets/",
				//		Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow);
				//}

				//webView2.NavigateToString(html);

				ManagedToLoadReadmeContents = true;
			}
			catch (Exception ex)
			{
				_logger?.Error(nameof(LoadRepositoryReadmeAsync), ex);

				ManagedToLoadReadmeContents = false;
			}
		}
	}
}
