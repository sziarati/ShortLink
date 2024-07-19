namespace Application.Results;

public class Result<T>  : AppResult
{
    public Result(bool isSuccess, Errors error) : base(isSuccess, error)
    {

    }
    public Result(bool isSuccess, T? data): base(isSuccess)
    {
        Data = data;
    }
    public Result(bool isSuccess, string message, T data):base(isSuccess, message)
    {
        Data = data;
    }
    
    public T? Data { get; }

    public static Result<T> Success(T data) => new Result<T>(true, data);
    public static Result<T> Failure(Errors error) => new Result<T>(false, error);
    public static Result<T> NotFound() => new Result<T>(false, Errors.NotFoundError);
}
