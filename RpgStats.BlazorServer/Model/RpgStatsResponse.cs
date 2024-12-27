namespace RpgStats.BlazorServer.Model;

public class RpgStatsResponse<T>
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; } = "An error occurred";
    public T? Data { get; set; }
}