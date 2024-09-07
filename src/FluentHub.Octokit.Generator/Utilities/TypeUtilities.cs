// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.ModelGenerator.Models;
using Octokit.GraphQL.Core.Introspection;
using Octokit.GraphQL.Core.Utilities;
using System;

namespace FluentHub.Octokit.ModelGenerator.Utilities
{
	internal static class TypeUtilities
	{
		public static string GetCSharpLiteral(string value, TypeModel type)
		{
			if (type.Kind == TypeKind.Scalar)
			{
				if (type.Name == "String" || type.Name == "ID")
				{
					return value == null ? "null" : $"\"{value}\"";
				}
				else
				{
					return value;
				}
			}
			else if (type.Kind == TypeKind.Enum)
			{
				return $"{type.Name}.{value.SnakeCaseToPascalCase()}";
			}
			else if (type.Kind == TypeKind.NonNull)
			{
				return GetCSharpLiteral(value, type.OfType);
			}

			throw new NotImplementedException();
		}

		public static string GetCSharpReturnType(TypeModel type)
		{
			return GetCSharpType(ReduceType(type), type.Kind != TypeKind.NonNull, true);
		}

		public static string GetCSharpArgType(TypeModel type)
		{
			return GetCSharpType(ReduceType(type), type.Kind != TypeKind.NonNull, false);
		}

		public static string GetWrappedArgType(TypeModel type)
		{
			var csharpType = GetCSharpType(ReduceType(type), false, false);
			var nullable = type.Kind != TypeKind.NonNull ? "?" : string.Empty;
			return "Arg<" + csharpType + '>' + nullable;
		}

		public static string GetClassName(TypeModel type)
		{
			return PascalCase(type.Name);
		}

		public static string GetInterfaceName(TypeModel type)
		{
			return "I" + PascalCase(type.Name);
		}

		internal static object GetArgName(InputValueModel arg)
		{
			return arg.Name;
		}

		public static bool IsCSharpPrimitive(TypeModel type)
		{
			return type.Kind == TypeKind.Scalar ||
				type.Kind == TypeKind.Enum ||
				(type.Kind == TypeKind.NonNull && IsCSharpPrimitive(type.OfType));
		}

		public static string PascalCase(string value)
		{
			return value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);
		}

		/// <summary>
		/// Reduces a GraphQL type to one that can be represented in .NET.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>The reduced type.</returns>
		/// <remarks>
		/// The GraphQL type system can represent more types than .NET can - for example a
		/// non-null object type does not exist in .NET.
		/// </remarks>
		public static TypeModel ReduceType(TypeModel type)
		{
			if (type == null)
			{
				return null;
			}
			else if (type.Kind == TypeKind.NonNull)
			{
				if (IsValueType(type.OfType))
				{
					return type;
				}
				else
				{
					var result = type.OfType.Clone();
					result.OfType = ReduceType(result.OfType);
					return result;
				}
			}
			else if (type.Kind == TypeKind.List)
			{
				var result = type.Clone();
				result.OfType = ReduceType(result.OfType);
				return result;
			}

			return type;
		}

		private static string GetCSharpType(TypeModel type, bool nullableType, bool returnType)
		{
			switch (type.Kind)
			{
				case TypeKind.Scalar:
					var question = nullableType ? "?" : "";
					switch (type.Name)
					{
						case "Int": return "int" + question;
						case "Float": return "double" + question;
						case "String": return "string";
						case "Boolean": return "bool" + question;
						case "ID": return "ID" + question;
						case "GitTimeStamp": return "DateTimeOffset" + question;
						case "DateTime": return "DateTimeOffset" + question;
						default: return "string";
					}
				case TypeKind.Enum:
					return type.Name + (nullableType ? "?" : "");
				case TypeKind.Interface:
					return "I" + type.Name;
				case TypeKind.Object:
				case TypeKind.InputObject:
				case TypeKind.Union:
					return type.Name;
				case TypeKind.NonNull:
					return GetCSharpType(type.OfType, false, returnType);
				case TypeKind.List:
					return "List" + $"<{GetCSharpType(type.OfType, type.Kind != TypeKind.NonNull, false)}>";
				default:
					throw new NotSupportedException();
			}
		}

		private static bool IsValueType(TypeModel type)
		{
			return type.Kind == TypeKind.Enum ||
				(type.Kind == TypeKind.Scalar &&
					(type.Name == "Int" ||
					type.Name == "Float" ||
					type.Name == "Boolean" ||
					type.Name == "DateTime" ||
					type.Name == "ID"));
		}
	}
}
