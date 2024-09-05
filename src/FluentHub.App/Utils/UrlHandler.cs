namespace FluentHub.App.Utils
{
	// TODO: Not supported class
	public static class UrlHandler
	{
		/*
		public static FrameNavigationArgs GetFrameNavigationArgs(string url)
		{
			// The first validation
			if (url.StartsWith("https://github.com/") is false)
			{
				new ArgumentException($"The url \"{url}\" has bad syntax.");
				return null;
			}

			var segments = url.Split("/").ToList();
			FrameNavigationArgs args = new();

			segments.RemoveRange(0, 3);

			if (segments.Any() is true)
			{
				// Should be user login
				if (segments.Count >= 1)
				{
					args.Login = segments.ElementAt(0);

					// Should be repository name
					if (segments.Count >= 2)
					{
						args.Name = segments.ElementAt(1);

						if (segments.Count >= 3)
						{
							if (segments.Count >= 4)
							{
								switch (segments.ElementAt(2).ToLower())
								{
									case "issue":
									case "issues":
										args.PageType = UrlPageType.RepositoryOneIssue;
										break;
									case "pull":
										{
											if (segments.Count >= 5)
											{
												switch (segments.ElementAt(4))
												{
													case "commits":
														args.PageType = UrlPageType.RepositoryPullRequestCommits;
														break;
													case "checks":
														args.PageType = UrlPageType.RepositoryPullRequestCommits;
														break;
													case "files":
														args.PageType = UrlPageType.RepositoryPullRequestCommits;
														break;
													default:
														args.PageType = UrlPageType.Unknown;
														break;
												}
											}
											else
											{
												args.PageType = UrlPageType.RepositoryOnePullRequest;
											}
										}
										break;
									case "discussions":
										args.PageType = UrlPageType.RepositoryOneDiscussion;
										break;
									case "projects":
										args.PageType = UrlPageType.RepositoryOneProject;
										break;
									default:
										args.PageType = UrlPageType.Unknown;
										break;
								}
							}
							else
							{

							}
						}
					}
				}
			}

			return args;
		}
		*/
	}
}
