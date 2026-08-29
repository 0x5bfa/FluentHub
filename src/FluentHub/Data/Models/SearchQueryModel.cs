// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Models
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
