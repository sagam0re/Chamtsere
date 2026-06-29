namespace Chamtsere.Application.Common.Models;

public class Result
{
    internal Result(bool succeeded, object? data, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Data = data;
        Errors = [.. errors];
    }

    public bool Succeeded { get; init; }
    public object? Data { get; init; }
    public string[] Errors { get; init; }

    public static Result Success(object? data)
    {
        return new Result(true, data, []);
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, null, errors);
    }
}