namespace Application.Results;

public class AppResult
{
    public string Message { get; set; }
    public bool IsSuccess { get; }
    public Errors? Error { get; }
    public AppResult(bool isSuccess, Errors error)
    {
        IsSuccess = isSuccess;
        Error = error;
        Message = Error.Value;
    }
    public AppResult(bool isSuccess)
    {
        Message = string.Empty;
        IsSuccess = isSuccess;
    }
    public AppResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
}