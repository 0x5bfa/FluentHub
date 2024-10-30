// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Models
{
	public class CheckRunGroupModel
	{
		public string AppName { get; set; }

		public string AppDescription { get; set; }

		public ObservableCollection<CheckRunItemModel> CheckItems { get; set; }
	}
}
