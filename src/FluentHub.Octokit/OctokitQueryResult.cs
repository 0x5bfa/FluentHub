using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHub.Octokit
{
	public class OctokitQueryResult
	{
		public object? Response { get; set; }

		public PageInfo? PageInfo { get; set; }
	}
}
