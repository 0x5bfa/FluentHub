// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Models
{
	public class CheckRunGroupModel
	{
		public string AppName { get; set; } = default!;

		public string AppDescription { get; set; } = default!;

		public ObservableCollection<CheckRunItemModel> CheckItems { get; set; } = default!;
	}
}
