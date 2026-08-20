using FluentHub.Core.Extensions.Emoji;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class EmojiMappingTests
{
	[TestMethod]
	public void KnownShortCodeReturnsUnicode()
		=> Assert.AreEqual("😄", EmojiMapping.GetUnicode(":smile:"));

	[TestMethod]
	public void AliasReturnsSameUnicode()
		=> Assert.AreEqual(EmojiMapping.GetUnicode(":laughing:"), EmojiMapping.GetUnicode(":satisfied:"));

	[TestMethod]
	public void UnknownShortCodeReturnsEmptyString()
		=> Assert.AreEqual(string.Empty, EmojiMapping.GetUnicode(":not-a-real-emoji:"));
}
