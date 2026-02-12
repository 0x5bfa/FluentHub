# Copilot Review Instructions for FluentHub

## Localization and Resource File Rules

### 🚨 Critical: Non-en-US Resource File Changes

**日本語 / Japanese:**
翻訳リソースファイル（en-US以外の`.resw`ファイル）が変更されています。翻訳リソースファイルは直接変更しないでください。

**English:**
Translation resource files (non-en-US `.resw` files) have been modified. Please do not directly modify translation resource files.

**重要 / Important:**
- **変更すべきファイル / Files to modify:** `src/FluentHub.App/Strings/en-US/*.resw` のみ
- **翻訳の更新方法 / How translations are updated:** 他の言語の翻訳は、翻訳管理システム（Crowdin）を通じて自動的に更新されます
- **Crowdinの同期 / Crowdin sync:** このルールは、Crowdin からの自動同期（コミットメッセージに "Updated app translations by Crowdin" を含む）の場合は適用されません

### File Pattern to Monitor

Watch for changes to resource files matching these patterns:
- `src/FluentHub.App/Strings/*/Resources.resw` (excluding `en-US`)
- Any `.resw` or `.resx` files in language code directories (e.g., `ja-JP`, `fr-FR`, `de-DE`, `zh-CN`, etc.)

**Locale directories to monitor (excluding en-US):**
- `af-ZA`, `ar-SA`, `bg-BG`, `ca-ES`, `cs-CZ`, `da-DK`, `de-DE`, `el-GR`, `en-GB`
- `es-419`, `es-ES`, `fi-FI`, `fil-PH`, `fr-FR`, `he-IL`, `hi-IN`, `hu-HU`, `id-ID`
- `it-IT`, `ja-JP`, `ka-GE`, `ko-KR`, `lv-LV`, `nb-NO`, `nl-NL`, `nn-NO`, `no-NO`
- `pl-PL`, `pt-BR`, `pt-PT`, `ro-RO`, `ru-RU`, `sk-SK`, `sr-SP`, `sv-SE`, `ta-IN`
- `th-TH`, `tr-TR`, `uk-UA`, `vi-VN`, `zh-CN`, `zh-TW`

### Exception Rules

Do not flag this warning if:
- The commit message contains the phrase "Updated app translations by Crowdin" (case-insensitive substring match)
- The PR is from an automated bot account (e.g., `github-actions[bot]`, `crowdin-bot`, or similar automation accounts)
- The PR description explicitly mentions manual translation fix with justification

## Additional Review Guidelines for Resource Strings

### 1. Resource String Keys and Descriptions

When reviewing changes to `en-US` resource files, verify:

**✅ Good practices:**
- Each new string entry has a meaningful key that follows the project's naming convention
- String keys use PascalCase or descriptive identifiers (e.g., `MainPage_Title`, `Settings_Description`)
- Each resource string includes a `<comment>` element with context for translators
- Comments describe where and how the string is used

**❌ Issues to flag:**
- Generic or unclear key names (e.g., `String1`, `Text`, `Label`)
- Missing comments/descriptions for new strings
- Duplicate keys or values that could be consolidated

### 2. XAML Resource References

When XAML files are modified along with resource files:

**✅ Verify:**
- XAML elements use `x:Uid` attribute to reference localized strings
- The `x:Uid` value matches the resource key prefix in the `.resw` file
- Alternative: Elements use `x:Name` with proper code-behind binding for dynamic content

**Example patterns to check:**
```xml
<!-- Good: Using x:Uid for localization -->
<TextBlock x:Uid="MainPage_Title" />

<!-- Corresponding entry in Resources.resw -->
<!-- Key: MainPage_Title.Text -->
<!-- Value: Welcome to FluentHub -->

<!-- Also acceptable: x:Name with code-behind -->
<TextBlock x:Name="DynamicTitle" />
```

**❌ Issues to flag:**
- Hard-coded user-visible strings in XAML that should be localized
  - Examples to flag: Button labels, TextBlock content, error messages, tooltips, dialog titles
  - Acceptable hard-coded values:
    - Technical identifiers (e.g., "UTF-8")
    - Format strings (e.g., "{0:N2}")
    - Markup/HTML tags
    - xmlns namespace declarations (e.g., `xmlns:local="using:FluentHub.App"`)
    - Developer-only debug strings (e.g., "DEBUG:", log prefixes visible only in debug builds)
- `x:Uid` references that don't have corresponding entries in resource files
- Inconsistent naming between XAML `x:Uid` and resource keys

### 3. Resource File Structure

**✅ Check for:**
- Consistent XML formatting in `.resw` files
- All required elements: `<data>`, `<value>`, and `<comment>`
- No malformed XML or encoding issues
- Proper use of CDATA for strings containing special characters

### 4. String Content Quality

**日本語 / Japanese:**
新しい文字列を追加する際は、以下を確認してください：
- 文字列が明確で理解しやすい
- UIコンテキストに適している
- 適切な句読点と大文字小文字の使用
- UI要素の長さ制約を考慮する

**English:**
When adding new strings, ensure:
- Strings are clear and understandable
- Content is appropriate for the UI context
- Proper punctuation and capitalization are used
- Consider length constraints for UI elements

## Workflow Summary

### For Contributors:

1. **Adding new strings:** Only modify `src/FluentHub.App/Strings/en-US/*.resw`
2. **Updating existing strings:** Edit the en-US version only
3. **Translations:** After merging to main, Crowdin will automatically update other languages
4. **XAML files:** Use `x:Uid` or `x:Name` attributes appropriately

### For Reviewers:

1. Check that only `en-US` resource files are modified (unless it's a Crowdin sync)
2. Verify new strings have proper keys and translator comments
3. Ensure XAML changes properly reference resource strings
4. Confirm no hard-coded strings are introduced that should be localized

---

**Note:** These instructions help maintain consistent localization workflow and prevent translation synchronization issues. The translation management system (Crowdin) handles all non-en-US translations automatically.
