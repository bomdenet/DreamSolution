using System;

namespace Dream;

public sealed class Result<T>
{
    private T _value = default!;
    public T Value
    {
        get
        {
            if (_error != null)
                throw new InvalidOperationException("Cannot get value from a failed result.", _error);
            return _value;
        }
    }

    private Exception? _error;
    public Exception Error
    {
        get
        {
            if (_error == null)
                throw new InvalidOperationException("Cannot get error from a successful result.");
            return _error;
        }
    }

    public bool IsSuccess => _error == null;

    public static Result<T> Success(T value) => new() { _value = value };
    public static Result<T> Fail(Exception error) => new() { _error = error };
}
