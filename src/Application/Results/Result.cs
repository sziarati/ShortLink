namespace Application.Results;

public class Result<T> where T : class
{
    public Result(bool isSuccess, Errors error)
    {
        IsSuccess = isSuccess;
        Error = error;
        Message = Error.Value;
    }
    public Result(bool isSuccess, T? data = null)
    {
        IsSuccess = isSuccess;
        Message = string.Empty;
        Data = data;
    }
    public Result(bool isSuccess, string message, T data)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }
    public string Message { get; set; }
    public bool IsSuccess { get; }
    public Errors? Error { get; }
    public T? Data { get; }
    public static Result<T> Success(T data) => new Result<T>(true, data);
    public static Result<T> Failure(Errors error) => new Result<T>(false, error);
    public static Result<T> NotFound() => new Result<T>(false, Errors.NotFoundError);
}
