using FluentHub.Models;
using System.Text.Json.Serialization;

namespace FluentHub.Serialization
{
	[JsonSerializable(typeof(CustomThemeItem))]
	internal partial class AppJsonSerializerContext : JsonSerializerContext
	{
	}
}
