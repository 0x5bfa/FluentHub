using FluentHub.App.Models;
using System.Text.Json.Serialization;

namespace FluentHub.App.Serialization
{
	[JsonSerializable(typeof(CustomThemeItem))]
	internal partial class AppJsonSerializerContext : JsonSerializerContext
	{
	}
}
