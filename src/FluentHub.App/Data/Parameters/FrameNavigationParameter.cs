// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Data.Parameters
{
	public class FrameNavigationParameter
	{
		public string? UserLogin { get; set; }

		public string? RepositoryName { get; set; }

		public int Number { get; set; }

		public bool AsViewer { get; set; }

		public List<object>? Parameters { get; set; }
	}
}
