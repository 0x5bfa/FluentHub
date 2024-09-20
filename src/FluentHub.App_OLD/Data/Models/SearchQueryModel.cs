// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Models
{
	public class SearchQueryModel
	{
		public SearchQueryModel(string query, string label)
		{
			QueryString = query;
			Label = label;
		}

		public string QueryString { get; private set; }

		public string Label { get; private set; }
	}
}
