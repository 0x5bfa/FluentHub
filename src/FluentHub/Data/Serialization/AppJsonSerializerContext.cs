using FluentHub.Models;
using System.Text.Json.Serialization;

namespace FluentHub.Data.Serialization
{
	[JsonSerializable(typeof(CustomThemeItem))]
	internal partial class AppJsonSerializerContext : JsonSerializerContext
	{
	}
}
