// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Models
{
	public sealed class SearchCode
	{
		public Repository? Repository { get; set; }

		public string? Name { get; set; }

		public string? Path { get; set; }
	}
}
