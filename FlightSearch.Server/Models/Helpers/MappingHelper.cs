using FlightSearch.External.Amadeus.DTO;

namespace FlightSearch.Server.Models.Helpers;

public static class MappingHelper
{
    public static FlightSearchViewModel MapToViewModel(this FlightOffer flightOffer)
    {
        return new FlightSearchViewModel
        {
            DepartureAirport = flightOffer.Itineraries.First().Segments.First().Departure.IataCode,
            DepartureDate = flightOffer.Itineraries.First().Segments?.First()?.Departure.At.ToString() ?? "",
            ArivalAirport = flightOffer.Itineraries.First().Segments?.Last().Arrival.IataCode ?? "",
            ArivalDate = flightOffer.Itineraries.First().Segments?.Last().Arrival.At.ToString() ?? "",
            NumberOfPlaneChanges = (flightOffer.Itineraries.First().Segments.Count -1 ).ToString(),
            NumberOfPassengers = flightOffer.TravelerPricings.DistinctBy(_ => _.TravelerId).Count().ToString(),
            Currency = flightOffer.Price.Currency,
            TotalPrice = flightOffer.Price.GrandTotal,
            DepartureCarrier = flightOffer.Itineraries.First().Segments?.First().CarrierCode ?? "",
            ArrivalCarrier = flightOffer.Itineraries.First().Segments?.Last().CarrierCode ?? ""
        };
    }
    
    private static FlightSearchViewModel MapToViewModel(this ItineraryWithAdditional model)
    {
        return new FlightSearchViewModel
        {
            DepartureAirport = model.Itinerary.Segments.First().Departure.IataCode,
            DepartureDate = model.Itinerary.Segments?.First()?.Departure.At.ToString() ?? "",
            ArivalAirport = model.Itinerary.Segments?.Last().Arrival.IataCode ?? "",
            ArivalDate = model.Itinerary.Segments?.Last().Arrival.At.ToString() ?? "",
            NumberOfPlaneChanges = (model.Itinerary.Segments?.Count - 1).ToString() ?? "",
            NumberOfPassengers = model.NumberOfPeople.ToString(),
            Currency = model.Currency,
            TotalPrice = model.GrandTotal,
            DepartureCarrier = model.Itinerary.Segments?.First().CarrierCode ?? "",
            ArrivalCarrier = model.Itinerary.Segments?.Last().CarrierCode ?? ""
        };
    }
    
    public static List<FlightSearchViewModel> FlattenFlightOfferWithMultipleItineraries(this FlightOffer flightOffer)
    {
        var tmp = flightOffer.Itineraries.Select(iter => 
            new ItineraryWithAdditional 
                {Itinerary = iter, NumberOfPeople = flightOffer.TravelerPricings.DistinctBy(_ => _.TravelerId).Count(),
                    Currency = flightOffer.Price.Currency, GrandTotal = flightOffer.Price.GrandTotal}).ToList();
        return tmp.Select(_ => _.MapToViewModel()).ToList();
    }
    
    private class ItineraryWithAdditional
    {
        public Itineraries Itinerary { get; set; }
        public int NumberOfPeople { get; set; }
        public string Currency { get; set; }
        public string GrandTotal { get; set; }
    }
}

