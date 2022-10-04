using System.Globalization;

namespace FluentHub.Uwp.Models
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
                var systemDefaultLanguageOptionStr = "WndowsDefault".();
                Name = string.IsNullOrEmpty(systemDefaultLanguageOptionStr) ? "Windows Default" : systemDefaultLanguageOptionStr;
            }
        }

        public override string ToString() => Name;
    }
}
