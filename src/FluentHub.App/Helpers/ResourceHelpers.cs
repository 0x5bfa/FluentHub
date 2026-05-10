using Windows.ApplicationModel.Resources;
using Microsoft.UI.Xaml.Markup;

namespace FluentHub.App.Helpers
{
	[MarkupExtensionReturnType(ReturnType = typeof(string))]
	public sealed partial class ResourceString : MarkupExtension
	{
		private static readonly ResourceLoader _resourceLoader = new();

		public string? Name { get; set; }

		protected override object ProvideValue()
			=> _resourceLoader.GetString(Name) ?? string.Empty;
	}
}
