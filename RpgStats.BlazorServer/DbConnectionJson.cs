using Newtonsoft.Json;

namespace RpgStats.BlazorServer;
public class DbConnectionJson
{
    [JsonProperty("ConnectionStrings")]
    public ConnectionStrings? ConnectionStrings { get; set; }
}

public class ConnectionStrings
{

    [JsonProperty("RpgStatsPostgresql")]
    public string? RpgStatsPostgresql { get; set; }
}