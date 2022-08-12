namespace FluentHub.Uwp.Models
{
    public class CustomThemeItem
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string AbsolutePath { get; set; }

        public string Key { get => Name; }

        public bool IsImportedTheme { get; set; }
    }
}
