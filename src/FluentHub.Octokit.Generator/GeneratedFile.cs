// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using System;
using IoPath = System.IO.Path;

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
