﻿// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

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
