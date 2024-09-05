using Microsoft.Windows.ApplicationModel.Resources;
using System.Collections.Concurrent;

namespace FluentHub.App.Extensions
{
	public static class StringExtensions
	{
		private static readonly ResourceMap resourcesTree = new ResourceManager().MainResourceMap.TryGetSubtree("Resources");
		private static readonly ConcurrentDictionary<string, string> cachedResources = new ConcurrentDictionary<string, string>();

		public static string GetLocalizedResource(this string resourceKey)
		{
			if (cachedResources.TryGetValue(resourceKey, out var value))
			{
				return value;
			}
			value = resourcesTree?.TryGetValue(resourceKey)?.ValueAsString;
			return cachedResources[resourceKey] = value ?? string.Empty;
		}
	}
}
