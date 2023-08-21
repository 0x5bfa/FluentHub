// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.App.Models
{
	public class CheckRunGroupModel
	{
		public string AppName { get; set; }

		public string AppDescription { get; set; }

		public ObservableCollection<CheckRunItemModel> CheckItems { get; set; }
	}
}
