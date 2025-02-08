namespace MilkMan.Shared.Common;

public static class ResultExtensions
{
    public static T Match<T>(
        this Result<T> result,
        Func<T> onSuccess,
        Func<string, T> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.ErrorMessage);
    }
}