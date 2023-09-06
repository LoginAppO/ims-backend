using System.Text.Json.Serialization;

namespace Ims.SharedCore;

public class ApplicationResponse
{
    public ApplicationResponse(string? statusCode = null)
    {
        Succeeded = true;
        StatusCode = statusCode ?? "OK";
        Errors = Enumerable.Empty<string>();
    }

    public ApplicationResponse(string? statusCode, params string[] errors)
    {
        StatusCode = statusCode ?? "Fail";
        Errors = errors;
        Succeeded = !Errors.Any();
    }

    [JsonConstructor]
    public ApplicationResponse(string? statusCode, IEnumerable<string>? errors)
    {
        StatusCode = statusCode ?? "Fail";
        Errors = errors ?? Enumerable.Empty<string>();
        Succeeded = !Errors.Any();
    }

    public bool Succeeded { get; }

    public string StatusCode { get; }

    public IEnumerable<string> Errors { get; }

    public string ErrorsToString()
    {
        return Errors.Any() ? string.Join(", ", Errors) : string.Empty;
    }

    public static ApplicationResponse Success()
    {
        return new ApplicationResponse();
    }

    public static ApplicationResponse<T> Success<T>(T data)
    {
        return new ApplicationResponse<T>(data);
    }

    public static ApplicationResponse Fail(params string[] errors)
    {
        return new ApplicationResponse("Fail", errors);
    }

    public static ApplicationResponse Fail(IEnumerable<string> errors)
    {
        return new ApplicationResponse("Fail", errors);
    }

    public static ApplicationResponse BadRequest(params string[] errors)
    {
        return new ApplicationResponse("BadRequest", errors);
    }

    public static ApplicationResponse BadRequest(IEnumerable<string> errors)
    {
        return new ApplicationResponse("BadRequest", errors);
    }

    public static ApplicationResponse NotFound(params string[] errors)
    {
        return new ApplicationResponse("NotFound", errors);
    }

    public static ApplicationResponse NotFound(IEnumerable<string> errors)
    {
        return new ApplicationResponse("NotFound", errors);
    }

    public static ApplicationResponse Fail(string? statusCode, params string[] errors)
    {
        return new ApplicationResponse(statusCode ?? "Fail", errors);
    }

    public static ApplicationResponse Fail(string? statusCode, IEnumerable<string> errors)
    {
        return new ApplicationResponse(statusCode ?? "Fail", errors);
    }

    public static ApplicationResponse<T> Fail<T>(params string[] errors)
    {
        return new ApplicationResponse<T>("Fail", errors);
    }

    public static ApplicationResponse<T> Fail<T>(IEnumerable<string> errors)
    {
        return new ApplicationResponse<T>("Fail", errors);
    }

    public static ApplicationResponse<T> BadRequest<T>(params string[] errors)
    {
        return new ApplicationResponse<T>("BadRequest", errors);
    }

    public static ApplicationResponse<T> BadRequest<T>(IEnumerable<string> errors)
    {
        return new ApplicationResponse<T>("BadRequest", errors);
    }

    public static ApplicationResponse<T> NotFound<T>(params string[] errors)
    {
        return new ApplicationResponse<T>("NotFound", errors);
    }

    public static ApplicationResponse<T> NotFound<T>(IEnumerable<string> errors)
    {
        return new ApplicationResponse<T>("NotFound", errors);
    }

    public static ApplicationResponse<T> Fail<T>(string? statusCode, params string[] errors)
    {
        return new ApplicationResponse<T>(statusCode ?? "Fail", errors);
    }

    public static ApplicationResponse<T> Fail<T>(string? statusCode, IEnumerable<string> errors)
    {
        return new ApplicationResponse<T>(statusCode ?? "Fail", errors);
    }
}

public class ApplicationResponse<T> : ApplicationResponse
{
    public ApplicationResponse(T data, string? statusCode = null)
        : base(statusCode)
    {
        Data = data;
    }

    public ApplicationResponse(string? statusCode, IEnumerable<string>? errors)
        : base(statusCode, errors)
    {
    }

    [JsonConstructor]
    public ApplicationResponse(T? data, string? statusCode, IEnumerable<string>? errors)
        : base(statusCode, errors)
    {
        Data = data;
    }

    public T? Data { get; }
}
