// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Represents a type that can be retrieved by a URL.
	/// </summary>
	public interface IUniformResourceLocatable
	{
		/// <summary>
		/// The HTML path to this resource.
		/// </summary>
		string ResourcePath { get; set; }

		/// <summary>
		/// The URL to this resource.
		/// </summary>
		string Url { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class UniformResourceLocatable : IUniformResourceLocatable
	{
		public string ResourcePath { get; set; }

		public string Url { get; set; }
	}
}

