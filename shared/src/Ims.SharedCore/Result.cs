namespace Ims.SharedCore;

public readonly struct Result
{
    public Result()
    {
        (Succeeded, Errors) = (true, Enumerable.Empty<string>());
    }

    public Result(params string[] errors)
    {
        (Succeeded, Errors) = (false, errors ?? Enumerable.Empty<string>());
    }

    public Result(IEnumerable<string> errors)
    {
        (Succeeded, Errors) = (false, errors ?? Enumerable.Empty<string>());
    }

    public bool Succeeded { get; }

    public IEnumerable<string> Errors { get; }

    public string ErrorsToString()
    {
        return string.Join("; ", Errors);
    }
}

public readonly struct Result<T>
{
    public Result(T data)
    {
        if (data is null)
            throw new ArgumentNullException(nameof(data));

        (Succeeded, Data, Errors) = (true, data, Enumerable.Empty<string>());
    }

    public Result(params string[] errors)
    {
        (Succeeded, Data, Errors) = (false, default, errors ?? Enumerable.Empty<string>());
    }

    public Result(IEnumerable<string> errors)
    {
        (Succeeded, Data, Errors) = (false, default, errors ?? Enumerable.Empty<string>());
    }

    public bool Succeeded { get; }

    public T? Data { get; }

    public IEnumerable<string> Errors { get; }

    public string ErrorsToString()
    {
        return string.Join("; ", Errors);
    }
}
