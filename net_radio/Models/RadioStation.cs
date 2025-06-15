using Newtonsoft.Json;

public class RadioStation
{
    [JsonProperty("stationuuid")]
    public string? StationUuid { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("url_resolved")]
    public string? StreamUrl { get; set; }

    [JsonProperty("favicon")]
    public string? Favicon { get; set; }

    [JsonProperty("tags")]
    public string? Tags { get; set; }
}