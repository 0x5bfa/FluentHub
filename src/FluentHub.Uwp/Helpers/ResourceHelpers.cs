using Windows.ApplicationModel.Resources;
using Microsoft.UI.Xaml.Markup;

namespace FluentHub.Uwp.Helpers
{
    [MarkupExtensionReturnType(ReturnType = typeof(string))]
    public sealed class ResourceString : MarkupExtension
    {
        private static ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView();

        public string Name { get; set; }

        protected override object ProvideValue()
            => resourceLoader.GetString(this.Name);
    }
}
