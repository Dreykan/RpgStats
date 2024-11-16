using Newtonsoft.Json;

namespace RpgStats.BlazorServer;

public class DbConnectionJson
{
    [JsonProperty(nameof(ConnectionStrings))]
    public ConnectionStrings? ConnectionStrings { get; set; }
}

public class ConnectionStrings
{
    [JsonProperty(nameof(RpgStatsPostgresql))]
    public string? RpgStatsPostgresql { get; set; }
}