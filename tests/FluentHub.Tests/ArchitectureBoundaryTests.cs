using System.Text.RegularExpressions;
using FluentHub.Core.Application.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed partial class ArchitectureBoundaryTests
{
	[TestMethod]
	public void CoreDoesNotReferenceWindowsUiAssemblies()
	{
		var references = typeof(AppRoute).Assembly.GetReferencedAssemblies()
			.Select(reference => reference.Name)
			.Where(name => name is not null &&
				(name.StartsWith("Microsoft.UI", StringComparison.Ordinal)
				|| name.Contains("WindowsAppSDK", StringComparison.Ordinal)))
			.ToList();

		Assert.AreEqual(0, references.Count,
			$"FluentHub.Core references UI assemblies: {string.Join(", ", references)}");
	}

	[TestMethod]
	public void RoutesDoNotCarryInfrastructureModels()
	{
		var invalidProperties = typeof(AppRoute).Assembly.GetExportedTypes()
			.Where(type => type != typeof(AppRoute) && type.IsAssignableTo(typeof(AppRoute)))
			.SelectMany(type => type.GetProperties())
			.Where(property => GetReferencedTypes(property.PropertyType).Any(type =>
				type.Namespace?.StartsWith("FluentHub.Core.Infrastructure", StringComparison.Ordinal) == true))
			.Select(property => $"{property.DeclaringType!.Name}.{property.Name}")
			.ToList();

		Assert.AreEqual(0, invalidProperties.Count,
			$"Routes expose infrastructure models: {string.Join(", ", invalidProperties)}");
	}

	[TestMethod]
	public void PresentationUsesContentPresenterInsteadOfFrameOrPageControls()
	{
		var root = FindRepositoryRoot();
		var presentationRoot = Path.Combine(root, "src", "FluentHub");
		var sourceFiles = Directory.EnumerateFiles(presentationRoot, "*.*", SearchOption.AllDirectories)
			.Where(path => Path.GetExtension(path) is ".cs" or ".xaml")
			.Where(path => !HasPathSegment(path, "obj") && !HasPathSegment(path, "bin"));

		var violations = sourceFiles
			.SelectMany(path => ForbiddenNavigationPrimitiveRegex().Matches(File.ReadAllText(path))
				.Select(match => $"{Path.GetRelativePath(root, path)}: {match.Value}"))
			.ToList();

		Assert.AreEqual(0, violations.Count,
			$"Presentation navigation primitives remain:{Environment.NewLine}{string.Join(Environment.NewLine, violations)}");
	}

	[TestMethod]
	public void SourceContainsExpectedProductionProjects()
	{
		var root = FindRepositoryRoot();
		var projects = Directory.EnumerateFiles(Path.Combine(root, "src"), "*.csproj", SearchOption.AllDirectories)
			.Select(path => Path.GetRelativePath(root, path).Replace('\\', '/'))
			.OrderBy(path => path, StringComparer.Ordinal)
			.ToList();

		CollectionAssert.AreEqual(
			new[]
			{
				"src/FluentHub.Core/FluentHub.Core.csproj",
				"src/FluentHub/FluentHub.csproj",
				"src/Octokit/Octokit.csproj",
			},
			projects);
	}

	[TestMethod]
	public void OctokitRemainsOneNativeAotCompatibleClassLibrary()
	{
		var root = FindRepositoryRoot();
		var octokitRoot = Path.Combine(root, "src", "Octokit");
		var projects = Directory.EnumerateFiles(octokitRoot, "*.csproj", SearchOption.AllDirectories)
			.Where(path => !HasPathSegment(path, "obj") && !HasPathSegment(path, "bin"))
			.ToList();

		Assert.HasCount(1, projects);
		var project = File.ReadAllText(projects[0]);
		StringAssert.Contains(project, "<IsAotCompatible>true</IsAotCompatible>");
		StringAssert.Contains(project, "<VerifyReferenceAotCompatibility>true</VerifyReferenceAotCompatibility>");
		StringAssert.Contains(project, "<IsPackable>false</IsPackable>");
		StringAssert.Contains(project, "<Nullable>enable</Nullable>");
	}

	[TestMethod]
	public void RestClientRemainsNativeAotCompatible()
	{
		var root = FindRepositoryRoot();
		var restRoot = Path.Combine(root, "src", "Octokit", "Rest");

		var forbidden = new[]
		{
			"Activator.CreateInstance",
			"CancellationToken.None",
			"MakeGenericType",
			"SimpleJson",
			"Newtonsoft.Json",
		};
		var violations = Directory.EnumerateFiles(restRoot, "*.cs", SearchOption.AllDirectories)
			.Where(path => !HasPathSegment(path, "obj") && !HasPathSegment(path, "bin"))
			.SelectMany(path => forbidden
				.Where(value => File.ReadAllText(path).Contains(value, StringComparison.Ordinal))
				.Select(value => $"{Path.GetRelativePath(root, path)}: {value}"))
			.ToList();

		Assert.AreEqual(0, violations.Count,
			$"Native AOT-incompatible REST patterns remain:{Environment.NewLine}{string.Join(Environment.NewLine, violations)}");
	}

	[TestMethod]
	public void GraphQLClientRemainsNativeAotCompatible()
	{
		var root = FindRepositoryRoot();
		var graphQLRoot = Path.Combine(root, "src", "Octokit", "GraphQL");

		var forbidden = new[]
		{
			"Activator.CreateInstance",
			"Expression.Compile",
			"GraphQL.Client",
			"MakeGenericType",
			"Newtonsoft.Json",
		};
		var violations = Directory.EnumerateFiles(graphQLRoot, "*.cs", SearchOption.AllDirectories)
			.Where(path => !HasPathSegment(path, "obj") && !HasPathSegment(path, "bin"))
			.SelectMany(path => forbidden
				.Where(value => File.ReadAllText(path).Contains(value, StringComparison.Ordinal))
				.Select(value => $"{Path.GetRelativePath(root, path)}: {value}"))
			.ToList();

		Assert.AreEqual(0, violations.Count,
			$"Native AOT-incompatible GraphQL patterns remain:{Environment.NewLine}{string.Join(Environment.NewLine, violations)}");
	}

	[TestMethod]
	public void StaticGraphQLOperationsUseGeneratedMetadata()
	{
		var root = FindRepositoryRoot();
		var graphQLRoot = Path.Combine(root, "src", "FluentHub.Core", "Infrastructure", "GitHub");
		var violations = new List<string>();

		foreach (var path in Directory.EnumerateFiles(graphQLRoot, "*.cs", SearchOption.AllDirectories))
		{
			var source = File.ReadAllText(path);
			foreach (Match match in StaticGraphQLOperationRegex().Matches(source))
			{
				var precedingSource = source[..match.Index].TrimEnd();
				var previousLineStart = precedingSource.LastIndexOf('\n') + 1;
				var previousLine = precedingSource[previousLineStart..].Trim();
				if (!previousLine.StartsWith("[GeneratedGraphQLOperation<", StringComparison.Ordinal))
				{
					var line = source.AsSpan(0, match.Index).Count('\n') + 1;
					violations.Add($"{Path.GetRelativePath(root, path)}:{line} {match.Groups["name"].Value}");
				}
			}
		}

		Assert.AreEqual(0, violations.Count,
			$"Static GraphQL documents bypass generation:{Environment.NewLine}{string.Join(Environment.NewLine, violations)}");
	}

	[TestMethod]
	public void MainProjectCodeUsesOnlyApprovedFolders()
	{
		var root = FindRepositoryRoot();
		var allowedFolders = new HashSet<string>(StringComparer.Ordinal)
		{
			"Converters",
			"Controls",
			"Data",
			"Extensions",
			"Helpers",
			"Services",
			"Utils",
			"ViewModels",
			"Views",
		};
		var allowedRootFiles = new HashSet<string>(StringComparer.Ordinal)
		{
			"App.xaml",
			"App.xaml.cs",
			"GlobalUsings.cs",
			"Program.cs",
		};
		var violations = new List<string>();

		var projectRoot = Path.Combine(root, "src", "FluentHub");
		foreach (var path in Directory.EnumerateFiles(projectRoot, "*", SearchOption.AllDirectories)
			.Where(path => Path.GetExtension(path) is ".cs" or ".xaml")
			.Where(path => !HasPathSegment(path, "obj") && !HasPathSegment(path, "bin")))
		{
			var relativePath = Path.GetRelativePath(projectRoot, path);
			var segments = relativePath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
			if (segments.Length == 1
				? !allowedRootFiles.Contains(relativePath)
				: !allowedFolders.Contains(segments[0]))
			{
				violations.Add(Path.GetRelativePath(root, path));
			}
		}

		Assert.AreEqual(0, violations.Count,
			$"Main project code exists outside the approved folders:{Environment.NewLine}{string.Join(Environment.NewLine, violations)}");
	}

	[TestMethod]
	public void PersonPictureIsCentralizedInUserAvatar()
	{
		var root = FindRepositoryRoot();
		var presentationRoot = Path.Combine(root, "src", "FluentHub");
		var userAvatarPath = Path.Combine(
			presentationRoot,
			"Controls",
			"UserAvatar.cs");
		var violations = Directory.EnumerateFiles(presentationRoot, "*", SearchOption.AllDirectories)
			.Where(path => Path.GetExtension(path) is ".cs" or ".xaml")
			.Where(path => !HasPathSegment(path, "obj") && !HasPathSegment(path, "bin"))
			.Where(path => !string.Equals(path, userAvatarPath, StringComparison.OrdinalIgnoreCase))
			.SelectMany(path => DirectPersonPictureRegex().Matches(File.ReadAllText(path))
				.Select(match => $"{Path.GetRelativePath(root, path)}: {match.Value}"))
			.ToList();

		Assert.AreEqual(0, violations.Count,
			$"User avatars bypass the shared control:{Environment.NewLine}{string.Join(Environment.NewLine, violations)}");
		StringAssert.Contains(File.ReadAllText(userAvatarPath), "new PersonPicture");
	}

	private static string FindRepositoryRoot()
	{
		for (var directory = new DirectoryInfo(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
		{
			if (File.Exists(Path.Combine(directory.FullName, "FluentHub.slnx")))
				return directory.FullName;
		}

		throw new DirectoryNotFoundException("Could not locate the FluentHub repository root.");
	}

	private static bool HasPathSegment(string path, string segment)
		=> path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
			.Contains(segment, StringComparer.OrdinalIgnoreCase);

	private static IEnumerable<Type> GetReferencedTypes(Type type)
	{
		if (type.HasElementType)
		{
			foreach (var referencedType in GetReferencedTypes(type.GetElementType()!))
				yield return referencedType;
		}

		if (type.IsGenericType)
		{
			foreach (var argument in type.GetGenericArguments())
			{
				foreach (var referencedType in GetReferencedTypes(argument))
					yield return referencedType;
			}
		}

		yield return type;
	}

	[GeneratedRegex(@"<\s*(?:Frame|Page)\b|:\s*Page\b|\bMicrosoft\.UI\.Xaml\.Navigation\b|\bFrameNavigationParameter\b", RegexOptions.CultureInvariant)]
	private static partial Regex ForbiddenNavigationPrimitiveRegex();

	[GeneratedRegex(@"<\s*(?:PersonPicture|primer:Avatar)\b|new\s+PersonPicture\b", RegexOptions.CultureInvariant)]
	private static partial Regex DirectPersonPictureRegex();

	[GeneratedRegex("(?:private|public|internal)\\s+const\\s+string\\s+(?<name>\\w+)\\s*=\\s*\"\"\"\\s*(?:query|mutation|subscription)\\b", RegexOptions.CultureInvariant)]
	private static partial Regex StaticGraphQLOperationRegex();
}
