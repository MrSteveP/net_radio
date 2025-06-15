using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

public partial class RadioViewModel : ObservableObject
{
    private readonly RadioBrowserService _service = new();

    [ObservableProperty]
    private List<RadioStation> stations = new();

    [ObservableProperty]
    private RadioStation? selectedStation;

    [ObservableProperty]
    private string? currentStreamUrl;

    [ObservableProperty]
    private bool isPlaying;

    [ObservableProperty]
    private string? searchText;

    [ObservableProperty]
    private ObservableCollection<RadioStation> filteredStations = new();


    public RadioViewModel()
    {
        _ = LoadStationsAsync();
    }

    [RelayCommand]
    public async Task LoadStationsAsync()
    {
        Stations = await _service.GetStationsAsync();
        FilteredStations = new ObservableCollection<RadioStation>(Stations);
    }

    [RelayCommand]
    public void PlayStation(RadioStation? station)
    {
        if (station is null)
            return;

        SelectedStation = station;
        this.CurrentStreamUrl = station.StreamUrl;
        this.IsPlaying = !string.IsNullOrEmpty(station.StreamUrl);
        // System.Diagnostics.Debug.WriteLine($"Station Uuid: {station.StationUuid}");
        // System.Diagnostics.Debug.WriteLine($"Playing: {station.StreamUrl}");
        // System.Diagnostics.Debug.WriteLine($"IsPlaying: {IsPlaying}");
    }

    partial void OnSearchTextChanged(string? value)
    {
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredStations = new ObservableCollection<RadioStation>(Stations);
        }
        else
        {
            var lower = SearchText.ToLowerInvariant();
            FilteredStations = new ObservableCollection<RadioStation>(
                Stations.Where(s =>
                    (!string.IsNullOrEmpty(s.Name) && s.Name.ToLowerInvariant().Contains(lower)) ||
                    (!string.IsNullOrEmpty(s.Tags) && s.Tags.ToLowerInvariant().Contains(lower))
                )
            );
        }
    }
}