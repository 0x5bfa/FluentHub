using FluentHub.ViewModels.Repositories;
using FluentHub.Utils;

namespace FluentHub.ViewModels.UserControls
{
	public class ReadmeContentBlockViewModel : ObservableObject
	{
		private readonly IFluentHubGitHubClient _gitHub;
		private readonly ILogger? _logger;

		private RepoContextViewModel _contextViewModel = default!;
		private bool _managedToLoadReadmeContents;
		private string? _baseUrl;
		private string _markdown = string.Empty;

		public ReadmeContentBlockViewModel(IFluentHubGitHubClient gitHub, ILogger? logger = null)
		{
			_gitHub = gitHub;
			_logger = logger;
		}

		public RepoContextViewModel ContextViewModel
		{
			get => _contextViewModel;
			set => SetProperty(ref _contextViewModel, value);
		}

		public bool ManagedToLoadReadmeContents
		{
			get => _managedToLoadReadmeContents;
			set => SetProperty(ref _managedToLoadReadmeContents, value);
		}

		public string? BaseUrl
		{
			get => _baseUrl;
			set => SetProperty(ref _baseUrl, value);
		}

		public string Markdown
		{
			get => _markdown;
			set => SetProperty(ref _markdown, value);
		}

		public async Task LoadRepositoryReadmeAsync()
		{
			ManagedToLoadReadmeContents = false;
			Markdown = string.Empty;

			try
			{
				var owner = ContextViewModel.Repository.Owner.Login;
				var name = ContextViewModel.Repository.Name;
				var branch = ContextViewModel.BranchName;

				Markdown = await _gitHub.Repositories.Repositories.GetReadmeMarkdownAsync(owner, name);
				BaseUrl = $"https://raw.githubusercontent.com/{owner}/{name}/{branch}/";
				ManagedToLoadReadmeContents = !string.IsNullOrWhiteSpace(Markdown);
			}
			catch (Exception ex)
			{
				_logger?.Error(nameof(LoadRepositoryReadmeAsync), ex);
				ManagedToLoadReadmeContents = false;
			}
		}
	}
}
