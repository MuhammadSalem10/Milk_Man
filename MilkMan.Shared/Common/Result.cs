
namespace MilkMan.Shared.Common;

public class Result
{
    protected Result(bool isSuccess, string? error)
    {
        if (isSuccess && error != null ||
            !isSuccess && string.IsNullOrEmpty(error))
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        ErrorMessage = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;
    public string? ErrorMessage { get; }

    public static Result Success() => new(true, null);

    public static Result Failure(string error) => new(false, error);

   
}


public class Result<T> : Result
{
    protected Result(bool isSuccess, T value, string? errorMessage) : base(isSuccess, errorMessage)
    {

        Value = value;
    }

    public T Value { get; }

    public static Result<T> Success(T value) => new(true, value, null);
    public static new Result<T> Failure(string message) => new(false, default, message);
}

