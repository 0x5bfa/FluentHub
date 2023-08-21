// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Extensions;
using System.Globalization;

namespace FluentHub.App.Models
{
	public class DefaultLanguageModel
	{
		public string ID { get; set; }

		public string Name { get; set; }

		public DefaultLanguageModel(string id)
		{
			if (!string.IsNullOrEmpty(id))
			{
				var info = new CultureInfo(id);

				ID = info.Name;
				Name = info.NativeName;
			}
			else
			{
				ID = string.Empty;
				var systemDefaultLanguageOptionStr = "WndowsDefault".GetLocalizedResource();
				Name = string.IsNullOrEmpty(systemDefaultLanguageOptionStr) ? "Windows Default" : systemDefaultLanguageOptionStr;
			}
		}

		public override string ToString() => Name;
	}
}
