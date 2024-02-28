using System.Text.Json.Serialization;

namespace FlightSearch.Server.Models;

public class FlightSearchViewModel
{
    [JsonPropertyName("departureAirport")]
    public string DepartureAirport { get; set; }
    [JsonPropertyName("departureDate")]
    public string DepartureDate { get; set; }
    [JsonPropertyName("arrivalAirport")]
    public string ArivalAirport { get; set; }
    [JsonPropertyName("arrivalDate")]
    public string ArivalDate { get; set; }
    [JsonPropertyName("numberOfPlaneChanges")]
    public string NumberOfPlaneChanges { get; set; }
    [JsonPropertyName("numberOfPassengers")]
    public string NumberOfPassengers { get; set; }
    [JsonPropertyName("totalPrice")]
    public string TotalPrice { get; set; }
    [JsonPropertyName("currency")]
    public string Currency { get; set; }
    [JsonPropertyName("departureCarrier")]
    public string DepartureCarrier { get; set; }
    [JsonPropertyName("arrivalCarrier")]
    public string ArrivalCarrier { get; set; }
}

