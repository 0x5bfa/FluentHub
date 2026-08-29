// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace Octokit.Generators;

internal static class GraphQLDocumentParser
{
	public static GraphQLDocument Parse(string source)
	{
		if (string.IsNullOrWhiteSpace(source))
			return GraphQLDocument.Invalid("the document is empty");

		var tokenization = Tokenize(source);
		if (tokenization.Error is not null)
			return GraphQLDocument.Invalid(tokenization.Error);

		var tokens = tokenization.Tokens;
		var delimiterError = ValidateDelimiters(tokens);
		if (delimiterError is not null)
			return GraphQLDocument.Invalid(delimiterError);

		var definitions = new List<Definition>();
		var fragmentDefinitions = new HashSet<string>(StringComparer.Ordinal);
		var fragmentReferences = new HashSet<string>(StringComparer.Ordinal);
		var declaredVariables = new HashSet<string>(StringComparer.Ordinal);
		var usedVariables = new HashSet<string>(StringComparer.Ordinal);
		var variableDefinitionTokens = new HashSet<int>();

		for (var index = 0; index < tokens.Count;)
		{
			var token = tokens[index];
			if (token.Value == "fragment")
			{
				if (!TryReadName(tokens, index + 1, out var fragmentName) ||
					!TryReadValue(tokens, index + 2, "on") ||
					!TryReadName(tokens, index + 3, out _))
				{
					return InvalidAt(token, "invalid fragment definition");
				}

				if (!fragmentDefinitions.Add(fragmentName))
					return InvalidAt(token, $"fragment '{fragmentName}' is defined more than once");

				var selectionIndex = FindNext(tokens, index + 4, "{");
				if (selectionIndex < 0 || !TryFindMatching(tokens, selectionIndex, "{", "}", out var endIndex))
					return InvalidAt(token, $"fragment '{fragmentName}' has no complete selection set");

				definitions.Add(new Definition(isOperation: false, null, null));
				index = endIndex + 1;
				continue;
			}

			if (token.Value is "query" or "mutation" or "subscription" || token.Value == "{")
			{
				var operationType = token.Value == "{" ? "Query" : ToPascalCase(token.Value);
				var cursor = index + 1;
				string? operationName = null;
				if (token.Value != "{" && TryReadName(tokens, cursor, out var parsedName))
				{
					operationName = parsedName;
					cursor++;
				}

				if (cursor < tokens.Count && tokens[cursor].Value == "(")
				{
					if (!TryFindMatching(tokens, cursor, "(", ")", out var variablesEnd))
						return InvalidAt(tokens[cursor], "the variable definition list is not closed");

					for (var variableIndex = cursor + 1; variableIndex < variablesEnd; variableIndex++)
					{
						if (tokens[variableIndex].Value != "$")
							continue;
						if (!TryReadName(tokens, variableIndex + 1, out var variableName) ||
							!TryReadValue(tokens, variableIndex + 2, ":"))
						{
							return InvalidAt(tokens[variableIndex], "invalid variable definition");
						}

						if (!declaredVariables.Add(variableName))
							return InvalidAt(tokens[variableIndex], $"variable '${variableName}' is defined more than once");
						variableDefinitionTokens.Add(variableIndex);
						variableDefinitionTokens.Add(variableIndex + 1);
					}

					cursor = variablesEnd + 1;
				}

				var selectionIndex = token.Value == "{" ? index : FindNext(tokens, cursor, "{");
				if (selectionIndex < 0 || !TryFindMatching(tokens, selectionIndex, "{", "}", out var endIndex))
					return InvalidAt(token, "the operation has no complete selection set");

				definitions.Add(new Definition(isOperation: true, operationType, operationName));
				index = endIndex + 1;
				continue;
			}

			return InvalidAt(token, $"unexpected top-level token '{token.Value}'");
		}

		var operations = definitions.Where(definition => definition.IsOperation).ToList();
		if (operations.Count != 1)
			return GraphQLDocument.Invalid($"exactly one operation is required, but {operations.Count} were found");

		for (var index = 0; index < tokens.Count - 1; index++)
		{
			if (tokens[index].Value == "$" && !variableDefinitionTokens.Contains(index) &&
				TryReadName(tokens, index + 1, out var variableName))
			{
				usedVariables.Add(variableName);
			}

			if (tokens[index].Value == "..." && TryReadName(tokens, index + 1, out var fragmentName) &&
				fragmentName != "on")
			{
				fragmentReferences.Add(fragmentName);
			}
		}

		var undeclaredVariable = usedVariables.FirstOrDefault(variable => !declaredVariables.Contains(variable));
		if (undeclaredVariable is not null)
			return GraphQLDocument.Invalid($"variable '${undeclaredVariable}' is used but not declared");

		var unusedVariable = declaredVariables.FirstOrDefault(variable => !usedVariables.Contains(variable));
		if (unusedVariable is not null)
			return GraphQLDocument.Invalid($"variable '${unusedVariable}' is declared but not used");

		var missingFragment = fragmentReferences.FirstOrDefault(fragment => !fragmentDefinitions.Contains(fragment));
		if (missingFragment is not null)
			return GraphQLDocument.Invalid($"fragment '{missingFragment}' is referenced but not defined");

		var unusedFragment = fragmentDefinitions.FirstOrDefault(fragment => !fragmentReferences.Contains(fragment));
		if (unusedFragment is not null)
			return GraphQLDocument.Invalid($"fragment '{unusedFragment}' is defined but not used");

		var operation = operations[0];
		return GraphQLDocument.Valid(operation.Name ?? "Anonymous", operation.Type!);
	}

	private static TokenizationResult Tokenize(string source)
	{
		var tokens = new List<Token>();
		var line = 1;
		var column = 1;

		for (var index = 0; index < source.Length;)
		{
			var character = source[index];
			if (character is ' ' or '\t' or '\r' or '\n' or ',')
			{
				Advance(character, ref line, ref column);
				index++;
				continue;
			}

			if (character == '#')
			{
				while (index < source.Length && source[index] is not ('\r' or '\n'))
				{
					index++;
					column++;
				}
				continue;
			}

			var tokenLine = line;
			var tokenColumn = column;
			if (character == '"')
			{
				var isBlock = index + 2 < source.Length && source[index + 1] == '"' && source[index + 2] == '"';
				var terminatorLength = isBlock ? 3 : 1;
				for (var count = 0; count < terminatorLength; count++)
				{
					index++;
					column++;
				}

				var terminated = false;
				while (index < source.Length)
				{
					if (isBlock && index + 2 < source.Length && source[index] == '"' &&
						source[index + 1] == '"' && source[index + 2] == '"' &&
						(index == 0 || source[index - 1] != '\\'))
					{
						index += 3;
						column += 3;
						terminated = true;
						break;
					}

					if (!isBlock && source[index] == '"' && (index == 0 || source[index - 1] != '\\'))
					{
						index++;
						column++;
						terminated = true;
						break;
					}

					if (!isBlock && source[index] is '\r' or '\n')
						return TokenizationResult.Invalid($"unterminated string at line {tokenLine}, column {tokenColumn}");

					Advance(source[index], ref line, ref column);
					index++;
				}

				if (!terminated)
					return TokenizationResult.Invalid($"unterminated string at line {tokenLine}, column {tokenColumn}");

				tokens.Add(new Token("<string>", tokenLine, tokenColumn));
				continue;
			}

			if (character == '.' && index + 2 < source.Length && source[index + 1] == '.' && source[index + 2] == '.')
			{
				tokens.Add(new Token("...", tokenLine, tokenColumn));
				index += 3;
				column += 3;
				continue;
			}

			if (char.IsLetter(character) || character == '_')
			{
				var start = index;
				while (index < source.Length && (char.IsLetterOrDigit(source[index]) || source[index] == '_'))
				{
					index++;
					column++;
				}
				tokens.Add(new Token(source.Substring(start, index - start), tokenLine, tokenColumn));
				continue;
			}

			if (char.IsDigit(character) || character == '-')
			{
				var start = index;
				while (index < source.Length && (char.IsLetterOrDigit(source[index]) || source[index] is '-' or '+' or '.'))
				{
					index++;
					column++;
				}
				tokens.Add(new Token(source.Substring(start, index - start), tokenLine, tokenColumn));
				continue;
			}

			if ("!$():=@[]{|}&".IndexOf(character) >= 0)
			{
				tokens.Add(new Token(character.ToString(), tokenLine, tokenColumn));
				index++;
				column++;
				continue;
			}

			return TokenizationResult.Invalid($"unexpected character '{character}' at line {line}, column {column}");
		}

		return TokenizationResult.Valid(tokens);
	}

	private static string? ValidateDelimiters(IReadOnlyList<Token> tokens)
	{
		var delimiters = new Stack<Token>();
		foreach (var token in tokens)
		{
			if (token.Value is "{" or "(" or "[")
			{
				delimiters.Push(token);
				continue;
			}

			if (token.Value is not ("}" or ")" or "]"))
				continue;
			if (delimiters.Count == 0)
				return $"unexpected closing delimiter '{token.Value}' at line {token.Line}, column {token.Column}";

			var opening = delimiters.Pop();
			string expected;
			switch (opening.Value)
			{
				case "{":
					expected = "}";
					break;
				case "(":
					expected = ")";
					break;
				default:
					expected = "]";
					break;
			}
			if (token.Value != expected)
			{
				return $"delimiter '{opening.Value}' at line {opening.Line}, column {opening.Column} " +
					$"is closed by '{token.Value}'";
			}
		}

		if (delimiters.Count == 0)
			return null;

		var unclosed = delimiters.Peek();
		return $"delimiter '{unclosed.Value}' at line {unclosed.Line}, column {unclosed.Column} is not closed";
	}

	private static bool TryFindMatching(
		IReadOnlyList<Token> tokens,
		int start,
		string open,
		string close,
		out int end)
	{
		var depth = 0;
		for (var index = start; index < tokens.Count; index++)
		{
			if (tokens[index].Value == open)
				depth++;
			else if (tokens[index].Value == close && --depth == 0)
			{
				end = index;
				return true;
			}
		}

		end = -1;
		return false;
	}

	private static int FindNext(IReadOnlyList<Token> tokens, int start, string value)
	{
		for (var index = start; index < tokens.Count; index++)
		{
			if (tokens[index].Value == value)
				return index;
		}
		return -1;
	}

	private static bool TryReadName(IReadOnlyList<Token> tokens, int index, out string value)
	{
		if (index < tokens.Count && tokens[index].Value.Length > 0 &&
			(char.IsLetter(tokens[index].Value[0]) || tokens[index].Value[0] == '_'))
		{
			value = tokens[index].Value;
			return true;
		}

		value = string.Empty;
		return false;
	}

	private static bool TryReadValue(IReadOnlyList<Token> tokens, int index, string value)
	{
		return index < tokens.Count && tokens[index].Value == value;
	}

	private static GraphQLDocument InvalidAt(Token token, string message)
	{
		return GraphQLDocument.Invalid($"{message} at line {token.Line}, column {token.Column}");
	}

	private static string ToPascalCase(string value)
	{
		return char.ToUpperInvariant(value[0]) + value.Substring(1);
	}

	private static void Advance(char character, ref int line, ref int column)
	{
		if (character == '\n')
		{
			line++;
			column = 1;
		}
		else if (character != '\r')
		{
			column++;
		}
	}

	private sealed class Definition
	{
		public Definition(bool isOperation, string? type, string? name)
		{
			IsOperation = isOperation;
			Type = type;
			Name = name;
		}

		public bool IsOperation { get; }
		public string? Type { get; }
		public string? Name { get; }
	}

	private sealed class Token
	{
		public Token(string value, int line, int column)
		{
			Value = value;
			Line = line;
			Column = column;
		}

		public string Value { get; }
		public int Line { get; }
		public int Column { get; }
	}

	private sealed class TokenizationResult
	{
		private TokenizationResult(List<Token> tokens, string? error)
		{
			Tokens = tokens;
			Error = error;
		}

		public List<Token> Tokens { get; }
		public string? Error { get; }

		public static TokenizationResult Valid(List<Token> tokens)
		{
			return new(tokens, error: null);
		}

		public static TokenizationResult Invalid(string error)
		{
			return new([], error);
		}
	}
}

internal sealed class GraphQLDocument
{
	private GraphQLDocument(bool isValid, string? operationName, string? operationType, string? error)
	{
		IsValid = isValid;
		OperationName = operationName ?? string.Empty;
		OperationType = operationType ?? string.Empty;
		Error = error ?? string.Empty;
	}

	public bool IsValid { get; }
	public string OperationName { get; }
	public string OperationType { get; }
	public string Error { get; }

	public static GraphQLDocument Valid(string operationName, string operationType)
	{
		return new(isValid: true, operationName, operationType, error: null);
	}

	public static GraphQLDocument Invalid(string error)
	{
		return new(isValid: false, operationName: null, operationType: null, error);
	}
}
