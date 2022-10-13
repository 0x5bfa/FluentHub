namespace FluentHub.Octokit.ModelGenerator
{
    internal static class AttributeGenerator
    {
        public static string GenerateObsoleteAttribute(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                return "[Obsolete]";
            }

            return $"[Obsolete(@\"{reason}\")]";
        }
    }
}
