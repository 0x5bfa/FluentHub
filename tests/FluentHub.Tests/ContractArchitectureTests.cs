using FluentHub.Octokit.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class ContractArchitectureTests
{
	private const string ContractNamespace = "FluentHub.Octokit.Contracts";
	private const string TransportModelNamespace = "Octokit.GraphQL.Model";

	[TestMethod]
	public void ContractsStayFocusedOnApplicationData()
	{
		var contractTypes = GetContractTypes();

		Assert.IsTrue(contractTypes.Count < 500, "The contract layer should not mirror the complete GitHub schema.");
		Assert.IsFalse(contractTypes.Any(type =>
			type.Name.EndsWith("Input", StringComparison.Ordinal)
			|| type.Name.EndsWith("Payload", StringComparison.Ordinal)),
			"Mutation boundaries should use application Request and Result contracts.");
	}

	[TestMethod]
	public void ContractsDoNotExposeOctokitTransportModels()
	{
		var exposedTransportProperties = GetContractTypes()
			.SelectMany(type => type.GetProperties())
			.Where(property => GetReferencedTypes(property.PropertyType)
				.Any(type => type.Namespace == TransportModelNamespace))
			.Select(property => $"{property.DeclaringType!.Name}.{property.Name}")
			.ToList();

		Assert.AreEqual(0, exposedTransportProperties.Count,
			$"Contract properties expose transport models: {string.Join(", ", exposedTransportProperties)}");
	}

	private static List<Type> GetContractTypes()
		=> typeof(UpdateIssueRequest).Assembly.GetExportedTypes()
			.Where(type => type.Namespace == ContractNamespace)
			.ToList();

	private static IEnumerable<Type> GetReferencedTypes(Type type)
	{
		if (type.IsArray)
		{
			foreach (var referencedType in GetReferencedTypes(type.GetElementType()!))
				yield return referencedType;
			yield break;
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
}
