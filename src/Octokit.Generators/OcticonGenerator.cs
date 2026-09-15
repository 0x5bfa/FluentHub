// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text;
using System.Xml;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Octokit.Generators;

[Generator(LanguageNames.CSharp)]
public sealed class OcticonGenerator : IIncrementalGenerator
{
	private static readonly DiagnosticDescriptor InvalidSvg = new(
		"OCTICON001",
		"Invalid Octicon SVG",
		"Octicon SVG '{0}' could not be generated: {1}",
		"Octicons",
		DiagnosticSeverity.Error,
		isEnabledByDefault: true);

	private static readonly DiagnosticDescriptor DuplicateIcon = new(
		"OCTICON002",
		"Duplicate Octicon name",
		"Octicon SVG '{0}' produces the duplicate icon name '{1}'",
		"Octicons",
		DiagnosticSeverity.Error,
		isEnabledByDefault: true);

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var icons = context.AdditionalTextsProvider
			.Where(static file => file.Path.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
			.Select(static (file, cancellationToken) => Parse(file, cancellationToken))
			.Collect();

		context.RegisterSourceOutput(icons, static (sourceContext, results) =>
		{
			var validIcons = new List<SvgIcon>();
			var hasError = false;

			foreach (var result in results)
			{
				if (result.Diagnostic is { } diagnostic)
				{
					sourceContext.ReportDiagnostic(diagnostic);
					hasError = true;
					continue;
				}

				validIcons.Add(result.Icon!);
			}

			if (hasError || validIcons.Count == 0)
				return;

			var orderedIcons = validIcons
				.OrderBy(icon => icon.Identifier, StringComparer.Ordinal)
				.ToArray();

			var duplicate = orderedIcons
				.GroupBy(icon => icon.Identifier, StringComparer.Ordinal)
				.FirstOrDefault(group => group.Count() > 1);
			if (duplicate is not null)
			{
				foreach (var icon in duplicate)
					sourceContext.ReportDiagnostic(Diagnostic.Create(
						DuplicateIcon,
						Location.None,
						icon.SourceFileName,
						duplicate.Key));

				return;
			}

			var source = IconCodeGenerator.Generate(
				orderedIcons,
				"FluentHub.Controls",
				"Octicons");
			sourceContext.AddSource(
				"FluentHub.Controls.Octicons.g.cs",
				SourceText.From(source, Encoding.UTF8));
		});
	}

	private static ParseResult Parse(AdditionalText file, CancellationToken cancellationToken)
	{
		try
		{
			return new ParseResult(SvgIconReader.Read(file, cancellationToken), diagnostic: null);
		}
		catch (Exception exception) when (exception is InvalidDataException or FormatException or IOException or XmlException)
		{
			return new ParseResult(
				icon: null,
				diagnostic: Diagnostic.Create(
					InvalidSvg,
					Location.None,
					Path.GetFileName(file.Path),
					exception.Message));
		}
	}

	private sealed class ParseResult
	{
		public ParseResult(SvgIcon? icon, Diagnostic? diagnostic)
		{
			Icon = icon;
			Diagnostic = diagnostic;
		}

		public SvgIcon? Icon { get; }

		public Diagnostic? Diagnostic { get; }
	}
}
