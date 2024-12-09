namespace RpgStats.Services.Abstractions;

public class ServiceResult<T>
{
    public bool Success { get; private set; }
    public string? ErrorMessage { get; set; }
    public T? Data { get; private set; }

    public static ServiceResult<T> SuccessResult(T data)
    {
        return new ServiceResult<T> { Success = true, Data = data };
    }

    public static ServiceResult<T> ErrorResult(string errorMessage)
    {
        return new ServiceResult<T> { Success = false, ErrorMessage = errorMessage };
    }
}