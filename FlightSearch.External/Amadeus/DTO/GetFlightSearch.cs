using Refit;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlightSearch.External.Amadeus.DTO;

public class FlightSearchRequest : IEquatable<FlightSearchRequest>
{
    #region Equality

    public bool Equals(FlightSearchRequest? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(OriginLocationCode, other.OriginLocationCode, StringComparison.InvariantCultureIgnoreCase) &&
               string.Equals(DestinationLocationCode, other.DestinationLocationCode, StringComparison.InvariantCultureIgnoreCase) &&
               string.Equals(DepartureDate, other.DepartureDate, StringComparison.InvariantCultureIgnoreCase) &&
               string.Equals(ReturnDate, other.ReturnDate, StringComparison.InvariantCultureIgnoreCase) && Adults == other.Adults &&
               Children == other.Children && Infants == other.Infants && TravelClass == other.TravelClass &&
               string.Equals(IncludedAirlineCodes, other.IncludedAirlineCodes, StringComparison.InvariantCultureIgnoreCase) &&
               string.Equals(ExcludedAirlineCodes, other.ExcludedAirlineCodes, StringComparison.InvariantCultureIgnoreCase) && NonStop == other.NonStop &&
               string.Equals(CurrencyCode, other.CurrencyCode, StringComparison.InvariantCultureIgnoreCase) && MaxPrice == other.MaxPrice && Max == other.Max;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((FlightSearchRequest) obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(OriginLocationCode, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(DestinationLocationCode, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(DepartureDate, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(ReturnDate, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(Adults);
        hashCode.Add(Children);
        hashCode.Add(Infants);
        hashCode.Add(TravelClass);
        hashCode.Add(IncludedAirlineCodes, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(ExcludedAirlineCodes, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(NonStop);
        hashCode.Add(CurrencyCode, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(MaxPrice);
        hashCode.Add(Max);
        return hashCode.ToHashCode();
    }

    public static bool operator ==(FlightSearchRequest? left, FlightSearchRequest? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(FlightSearchRequest? left, FlightSearchRequest? right)
    {
        return !Equals(left, right);
    }

    #endregion

    [Required, AliasAs("originLocationCode"), JsonPropertyName("originLocationCode")]
    public string OriginLocationCode { get; init; }

    [Required, AliasAs("destinationLocationCode"), JsonPropertyName("destinationLocationCode")]
    public string DestinationLocationCode { get; init; }

    [Required, AliasAs("departureDate"), JsonPropertyName("departureDate")]
    public string DepartureDate { get; init; }

    [AliasAs("returnDate"), JsonPropertyName("returnDate")]
    public string? ReturnDate { get; init; }

    [Required, AliasAs("adults"), JsonPropertyName("adults")]
    public int Adults { get; init; }

    [AliasAs("children"), JsonPropertyName("children")]
    public int Children { get; init; }

    [AliasAs("infants"), JsonPropertyName("infants")]
    public int Infants { get; init; }

    [AliasAs("travelClass"), JsonPropertyName("travelClass")]
    public TravelClass? TravelClass { get; init; }

    [AliasAs("includedAirlineCodes"), JsonPropertyName("includedAirlineCodes")]
    public string? IncludedAirlineCodes { get; init; }

    [AliasAs("excludedAirlineCodes"), JsonPropertyName("excludedAirlineCodes")]
    public string? ExcludedAirlineCodes { get; init; }

    [AliasAs("nonStop"), JsonPropertyName("nonStop")]
    public bool NonStop { get; init; }

    [AliasAs("currencyCode"), JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; init; }

    [AliasAs("maxPrice"), JsonPropertyName("maxPrice")]
    public int MaxPrice { get; init; }

    [AliasAs("max"), JsonPropertyName("max")]
    public int Max { get; init; }
}

public enum TravelClass
{
    [System.Runtime.Serialization.EnumMember(Value = "ECONOMY")]
    Economy = 1,

    [System.Runtime.Serialization.EnumMember(Value = "PREMIUM_ECONOMY")]
    PremiumEconomy = 2,

    [System.Runtime.Serialization.EnumMember(Value = "BUSINESS")]
    Business = 3,

    [System.Runtime.Serialization.EnumMember(Value = "FIRST")]
    First = 4
}