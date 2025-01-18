namespace RpgStats.Faker;

public class RpgStatsResponse<T>
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; } = "An error occurred";
    public T? Data { get; set; }
}