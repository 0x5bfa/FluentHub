using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace Octokit.Generators;

internal static partial class SvgIconReader
{
	private static readonly Regex SizeSuffixRegex = new(
		"^(?<name>.+)-(?<size>[0-9]+)$",
		RegexOptions.Compiled | RegexOptions.CultureInvariant);

	public static SvgIcon Read(AdditionalText file, CancellationToken cancellationToken)
	{
		var filePath = file.Path;
		var sourceText = file.GetText(cancellationToken)
			?? throw new InvalidDataException($"'{filePath}' could not be read as source text.");
		var document = XDocument.Parse(sourceText.ToString(), LoadOptions.PreserveWhitespace);
		var root = document.Root ?? throw new InvalidDataException($"'{filePath}' has no SVG root element.");
		if (root.Name.LocalName != "svg")
			throw new InvalidDataException($"'{filePath}' does not have an SVG root element.");

		var viewBox = ReadViewBox(root, filePath);
		if (viewBox.X != 0 || viewBox.Y != 0 || viewBox.Width <= 0 || viewBox.Height <= 0)
			throw new InvalidDataException($"'{filePath}' must use a viewBox starting at 0,0 with positive dimensions.");

		var paths = new List<SvgPath>();
		foreach (var element in root.Elements())
		{
			switch (element.Name.LocalName)
			{
				case "path":
					paths.Add(ReadPath(element, filePath));
					break;
				case "title":
				case "desc":
				case "metadata":
					break;
				default:
					throw new InvalidDataException(
						$"'{filePath}' contains unsupported SVG element '{element.Name.LocalName}'.");
			}
		}

		if (paths.Count == 0)
			throw new InvalidDataException($"'{filePath}' does not contain any path elements.");

		var fileName = Path.GetFileNameWithoutExtension(filePath);
		var match = SizeSuffixRegex.Match(fileName);
		var iconName = match.Success ? match.Groups["name"].Value : fileName;
		var sizeSuffix = match.Success ? match.Groups["size"].Value : string.Empty;
		var identifier = ToIdentifier(iconName) + sizeSuffix;

		return new SvgIcon(
			identifier,
			Path.GetFileName(filePath),
			viewBox.Width,
			viewBox.Height,
			paths);
	}

	private static SvgPath ReadPath(XElement element, string filePath)
	{
		var supportedAttributes = new HashSet<string>(StringComparer.Ordinal)
		{
			"d",
			"fill",
			"fill-rule",
			"clip-rule",
		};

		foreach (var attribute in element.Attributes())
		{
			if (!supportedAttributes.Contains(attribute.Name.LocalName))
				throw new InvalidDataException(
					$"'{filePath}' contains unsupported path attribute '{attribute.Name.LocalName}'.");
		}

		var fill = (string?)element.Attribute("fill");
		if (!string.IsNullOrWhiteSpace(fill) && !IsSupportedFill(fill!))
			throw new InvalidDataException(
				$"'{filePath}' uses fill '{fill}', but PathIcon can only represent a monochrome fill.");

		var data = (string?)element.Attribute("d");
		if (string.IsNullOrWhiteSpace(data))
			throw new InvalidDataException($"'{filePath}' contains a path without a 'd' attribute.");

		var fillRule = ParseFillRule((string?)element.Attribute("fill-rule"), filePath);
		IReadOnlyList<SvgFigure> figures;
		try
		{
			figures = new SvgPathParser(data!).Parse();
		}
		catch (FormatException exception)
		{
			throw new InvalidDataException($"'{filePath}' contains an invalid path: {exception.Message}", exception);
		}

		return new SvgPath(fillRule, figures);
	}

	private static SvgFillRule ParseFillRule(string? value, string filePath)
	{
		if (string.IsNullOrWhiteSpace(value) || string.Equals(value, "nonzero", StringComparison.OrdinalIgnoreCase))
			return SvgFillRule.Nonzero;

		if (string.Equals(value, "evenodd", StringComparison.OrdinalIgnoreCase))
			return SvgFillRule.EvenOdd;

		throw new InvalidDataException($"'{filePath}' uses unsupported fill-rule '{value}'.");
	}

	private static bool IsSupportedFill(string value)
		=> value.Equals("currentColor", StringComparison.OrdinalIgnoreCase) ||
			value.Equals("black", StringComparison.OrdinalIgnoreCase) ||
			value.Equals("#000", StringComparison.OrdinalIgnoreCase) ||
			value.Equals("#000000", StringComparison.OrdinalIgnoreCase);

	private static ViewBox ReadViewBox(XElement root, string filePath)
	{
		var value = (string?)root.Attribute("viewBox");
		if (string.IsNullOrWhiteSpace(value))
			throw new InvalidDataException($"'{filePath}' does not have a viewBox attribute.");

		var parts = value!.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
		if (parts.Length != 4)
			throw new InvalidDataException($"'{filePath}' has an invalid viewBox '{value}'.");

		var numbers = new double[4];
		for (var index = 0; index < parts.Length; index++)
		{
			if (!double.TryParse(parts[index], NumberStyles.Float, CultureInfo.InvariantCulture, out numbers[index]))
				throw new InvalidDataException($"'{filePath}' has an invalid viewBox '{value}'.");
		}

		return new ViewBox(numbers[0], numbers[1], numbers[2], numbers[3]);
	}

	private static string ToIdentifier(string value)
	{
		var builder = new System.Text.StringBuilder();
		var capitalize = true;
		foreach (var character in value)
		{
			if (!char.IsLetterOrDigit(character))
			{
				capitalize = true;
				continue;
			}

			if (builder.Length == 0 && char.IsDigit(character))
				builder.Append("Icon");

			builder.Append(capitalize ? char.ToUpperInvariant(character) : character);
			capitalize = false;
		}

		if (builder.Length == 0)
			throw new InvalidDataException($"'{value}' is not a valid icon name.");

		return builder.ToString();
	}

	private struct ViewBox
	{
		public ViewBox(double x, double y, double width, double height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public double X { get; }

		public double Y { get; }

		public double Width { get; }

		public double Height { get; }
	}
}
