namespace RpgStats.WebApi;

public class ApiResponse<T>
{
    public bool Success { get; private set; }
    public string? ErrorMessage { get; private set; }
    public T? Data { get; private set; }

    private ApiResponse() {}

    public static ApiResponse<T> SuccessResult(T data)
    {
        return new ApiResponse<T> { Success = true, Data = data };
    }

    public static ApiResponse<T> ErrorResult(string errorMessage)
    {
        if (string.IsNullOrEmpty(errorMessage))
            errorMessage = "An error occurred.";

        return new ApiResponse<T> { Success = false, ErrorMessage = errorMessage };
    }

    public static ApiResponse<T> WarningResult(string warningMessage, T data)
    {
        if (string.IsNullOrEmpty(warningMessage))
            warningMessage = "A warning occurred.";

        return new ApiResponse<T> { Success = false, ErrorMessage = warningMessage, Data = data };
    }
}