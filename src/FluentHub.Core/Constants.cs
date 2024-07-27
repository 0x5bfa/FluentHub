// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

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
			public const string DocumentationUrl = @"https://github.com/0x5bfa/FluentHub/tree/main/docs";
			public const string ContributorUrl = @"https://github.com/0x5BFA/FluentHub/graphs/contributors";
			public const string FeedbackUrl = @"https://github.com/0x5BFA/FluentHub/issues/new/choose";
			public const string ReleaseNotesUrl = @"https://github.com/0x5BFA/FluentHub/releases";
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
