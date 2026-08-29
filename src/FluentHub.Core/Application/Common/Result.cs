// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Common;

public readonly record struct Result<T>
{
	private readonly T? _value;

	private Result(T value)
	{
		_value = value;
		Error = null;
		IsSuccess = true;
	}

	private Result(AppError error)
	{
		_value = default;
		Error = error ?? throw new ArgumentNullException(nameof(error));
		IsSuccess = false;
	}

	public bool IsSuccess { get; }

	public AppError? Error { get; }

	public T Value
		=> IsSuccess
			? _value!
			: throw new InvalidOperationException("A failed result does not contain a value.");

	public static Result<T> Success(T value)
		=> new(value);

	public static Result<T> Failure(AppError error)
		=> new(error);

	public bool TryGetValue(out T? value)
	{
		value = _value;
		return IsSuccess;
	}
}
