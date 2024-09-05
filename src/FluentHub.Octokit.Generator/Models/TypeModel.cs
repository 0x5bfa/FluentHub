// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Octokit.GraphQL.Core.Introspection;
using System.Collections.Generic;

namespace FluentHub.Octokit.ModelGenerator.Models
{
	public class TypeModel
	{
		public TypeKind Kind { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public IList<FieldModel> Fields { get; set; }

		public IList<TypeModel> Interfaces { get; set; }

		public IList<TypeModel> PossibleTypes { get; set; }

		public IList<EnumValueModel> EnumValues { get; set; }

		public IList<InputValueModel> InputFields { get; set; }

		public TypeModel OfType { get; set; }

		public TypeModel Clone()
		{
			return new TypeModel
			{
				Kind = Kind,
				Name = Name,
				Description = Description,
				Fields = Fields,
				Interfaces = Interfaces,
				PossibleTypes = PossibleTypes,
				EnumValues = EnumValues,
				InputFields = InputFields,
				OfType = OfType?.Clone(),
			};
		}

		public static TypeModel Boolean()
		{
			return new TypeModel
			{
				Kind = TypeKind.Scalar,
				Name = "Boolean",
			};
		}

		public static TypeModel Enum(string name)
		{
			return new TypeModel
			{
				Kind = TypeKind.Enum,
				Name = name,
			};
		}

		public static TypeModel Float()
		{
			return new TypeModel
			{
				Kind = TypeKind.Scalar,
				Name = "Float",
			};
		}

		public static TypeModel Int()
		{
			return new TypeModel
			{
				Kind = TypeKind.Scalar,
				Name = "Int",
			};
		}

		public static TypeModel ID()
		{
			return new TypeModel
			{
				Kind = TypeKind.Scalar,
				Name = "ID",
			};
		}

		public static TypeModel Interface(string name)
		{
			return new TypeModel
			{
				Kind = TypeKind.Interface,
				Name = name,
			};
		}

		public static TypeModel List(TypeModel ofType)
		{
			return new TypeModel
			{
				Kind = TypeKind.List,
				OfType = ofType,
			};
		}

		public static TypeModel NonNull(TypeModel ofType)
		{
			return new TypeModel
			{
				Kind = TypeKind.NonNull,
				OfType = ofType,
			};
		}

		public static TypeModel Object(string name)
		{
			return new TypeModel
			{
				Kind = TypeKind.Object,
				Name = name,
			};
		}

		public static TypeModel String()
		{
			return new TypeModel
			{
				Kind = TypeKind.Scalar,
				Name = "String",
			};
		}

		public static TypeModel DateTime()
		{
			return new TypeModel
			{
				Kind = TypeKind.Scalar,
				Name = "DateTime",
			};
		}

		public static TypeModel GitTimeStamp()
		{
			return new TypeModel
			{
				Kind = TypeKind.Scalar,
				Name = "GitTimeStamp",
			};
		}

		public static TypeModel Union(string name)
		{
			return new TypeModel
			{
				Kind = TypeKind.Union,
				Name = name,
			};
		}
	}
}
