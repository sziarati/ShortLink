namespace Domain.Results
{
    public class Result<T> where T : class
    {
        public Result(bool isSuccess, DomainErrors error, T? data = null) 
        {
            IsSuccess = isSuccess;
            Error = error;
            Data = data;
        }
        public Result(bool isSuccess, T? data = null)
        {
            IsSuccess = isSuccess;
            Data = data;
        }
        public bool IsSuccess { get; }
        public DomainErrors? Error { get; }
        public T? Data { get; }
        public static Result<T> Success(T data) => new Result<T>(true, data);
        public static Result<T> Failure(DomainErrors error, T data) => new Result<T>(false, error, data);
    }
}
