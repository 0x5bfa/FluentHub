namespace FluentHub.Core.Emoji
{
    public sealed class Emoji
    {
        public string Name { get; set; }
        public string EmojiUrl { get; set; }
        public string Value { get; set; }
        public bool Deprecated { get; set; }

        public static Emoji Define(string value, string name, bool deprecated = false)
        {
            return new Emoji
            {
                Name = name,
                Value = value,
                Deprecated = deprecated,
            };
        }
    }
}
