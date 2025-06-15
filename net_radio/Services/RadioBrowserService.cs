using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class RadioBrowserService
{
    private readonly HttpClient _httpClient = new();

    public async Task<List<RadioStation>> GetStationsAsync()
    {
        var url = "https://de1.api.radio-browser.info/json/stations/topclick/20";
        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return new List<RadioStation>();

        var json = await response.Content.ReadAsStringAsync();
        var stations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(json);

        var result = new List<RadioStation>();

        if (stations != null)
        {
            foreach (var s in stations)
            {
                result.Add(new RadioStation
                {
                    StationUuid = s.stationuuid,
                    Name = s.name,
                    StreamUrl = s.url_resolved,
                    Favicon = s.favicon,
                    Tags = s.tags
                });
            }
        }

        return result;
    }
}