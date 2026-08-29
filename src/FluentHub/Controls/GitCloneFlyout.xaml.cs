using FluentHub.Services;
using FluentHub.ViewModels.Repositories;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls
{
	public sealed partial class GitCloneFlyout : UserControl
	{
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(ViewModel),
				typeof(RepoContextViewModel),
				typeof(GitCloneFlyout),
				new PropertyMetadata(null));

		private string _repoGitUrl = string.Empty;
		private string _repoUrl = string.Empty;

		public RepoContextViewModel ViewModel
		{
			get => (RepoContextViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				DataContext = value;
			}
		}

		public GitCloneFlyout()
		{
			InitializeComponent();
		}

		private void OnGitCloneFlyoutLoaded(object sender, RoutedEventArgs e)
		{
			var owner = ViewModel.Repository.Owner.Login;
			var name = ViewModel.Repository.Name;

			_repoUrl = $"https://github.com/{owner}/{name}";
			_repoGitUrl = $"{_repoUrl}.git";

			var sshUrl = $"git@github.com:{owner}/{name}.git";
			var gitHubCliCommand = $"gh repo clone {owner}/{name}";

			HttpsCloneUriTextBox.Text = _repoGitUrl;
			HttpsCopyButton.ClipboardText = _repoGitUrl;
			SshCloneUriTextBox.Text = sshUrl;
			SshCopyButton.ClipboardText = sshUrl;
			GitHubCliCommandTextBox.Text = gitHubCliCommand;
			GitHubCliCopyButton.ClipboardText = gitHubCliCommand;
		}

		private async void OpenVSButton_Click(object sender, RoutedEventArgs e)
		{
			var encodedUrl = Uri.EscapeDataString(_repoGitUrl);
			var uri = new Uri($"git-client://clone?repo={encodedUrl}");

			if (!await Windows.System.Launcher.LaunchUriAsync(uri))
				Ioc.Default.GetService<Utils.ILogger>()?.Warn("Cannot open Visual Studio with URI {0}", uri);
		}

		private async void DownloadZipButton_Click(object sender, RoutedEventArgs e)
		{
			var downloadZip = $"{_repoUrl}/archive/refs/heads/{ViewModel.BranchName}.zip";
			var uri = new Uri(downloadZip);

			if (!await Windows.System.Launcher.LaunchUriAsync(uri))
				Ioc.Default.GetService<Utils.ILogger>()?.Error("Failed to download repository archive from {0}", downloadZip);
		}

		private async void GitHubDeskButton_Click(object sender, RoutedEventArgs e)
		{
			var gitHubDesktopUrl = $"x-github-client://openRepo/{_repoUrl}";
			var uri = new Uri(gitHubDesktopUrl);

			if (!await Windows.System.Launcher.LaunchUriAsync(uri))
				Ioc.Default.GetService<Utils.ILogger>()?.Warn("Cannot open GitHub Desktop with URI {0}", gitHubDesktopUrl);
		}
	}
}
