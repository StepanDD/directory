namespace DirectoryService.Domain.ValueObjects;

public record Address
{
    public Address(string country, string city, string street, string houseNumber, string? apartmentNumber)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be null or empty.", nameof(country));
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be null or empty.", nameof(city));
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street cannot be null or empty.", nameof(street));
        if (string.IsNullOrWhiteSpace(houseNumber))
            throw new ArgumentException("House number cannot be null or empty.", nameof(houseNumber));

        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        ApartmentNumber = apartmentNumber;
    }
    public string Country { get; }
    public string City { get; }
    public string Street { get; }
    public string HouseNumber { get; }
    public string? ApartmentNumber { get; }
    public Address Update(
        string? country = null,
        string? city = null,
        string? street = null,
        string? houseNumber = null,
        string? apartmentNumber = null)
    {
        return new Address(
            country ?? Country,
            city ?? City,
            street ?? Street,
            houseNumber ?? HouseNumber,
            apartmentNumber ?? ApartmentNumber);
    }
}
