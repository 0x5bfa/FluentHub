namespace FluentHub.Core
{
    public static class Constants
    {
        public static class App
        {
            public const string AppName = "FluentHub";
            public const string AppSuffix = "DEV";
            public const string AppDescription = "A fluent GitHub client for Windows";
        }

        public static class GitHub
        {
            public const string DocumentationUrl = @"https://github.com/FluentHub/FluentHub/docs";
            public const string ContributorUrl = @"https://github.com/FluentHub/FluentHub/graphs/contributors";
            public const string FeedbackUrl = @"https://github.com/FluentHub/FluentHub/issues/new/choose";
            public const string PrivacyPolicyUrl = @"https://github.com/FluentHub/FluentHub/blob/HEAD/docs/Privacy.md";
            public const string ReleaseNotesUrl = @"https://github.com/FluentHub/FluentHub/releases";
            public const string SupportUsUrl = @"https://github.com/FluentHub/FluentHub/sponsors";
        }

        public static class ResourcePaths
        {
            public const string WebViewIndexHtml = @"ms-appx:///Assets/WebView/index.html";
            public const string WebViewDarkModeCss = @"ms-appx:///Assets/WebView/github-markdown-dark.css";
            public const string WebViewLightModeCss = @"ms-appx:///Assets/WebView/github-markdown-light.css";
            public const string GlyphsPath = @"ms-appx:///Assets/Glyphs";
        }

        public static class LocalSettings
        {
            public const string UserSettingsFileName = @"UserSettings.json";
        }
    }
}
