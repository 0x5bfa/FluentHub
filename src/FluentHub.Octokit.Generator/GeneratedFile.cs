// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.ModelGenerator
{
	public class GeneratedFile
	{
		public string Path;

		public string Content;

		public GeneratedFile(string path, string content)
		{
			Path = path;
			Content = content;
		}
	}
}
