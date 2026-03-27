using System;

namespace Dream;

public sealed class Result<T>
{
    public T? Value { get; private set; }
    public Exception? Error { get; private set; }
    public bool IsSuccess => Error == null;

    public static Result<T> Ok(T value) => new() { Value = value };
    public static Result<T> Fail(Exception error) => new() { Error = error };
}
