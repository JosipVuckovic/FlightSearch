﻿using FlightSearch.External.Amadeus.DTO;
using Refit;

namespace FlightSearch.External.Amadeus.Services;

public interface IAmadeusApi
{
    //TODO JV: Do a proper model 
    
    [Headers("Authorization: Bearer")]
    [Get("/v2/shopping/flight-offers")]
    Task<string> GetFlightOffersAsync([Query] FlightSearchRequest request);
}