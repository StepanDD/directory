using System;
using Dapper;
using DirectoryService.Core.Locations;
using DirectoryService.Domain;
using DirectoryService.Infrastructure.Postgres.DataBase;

namespace DirectoryService.Infrastructure.Postgres.Locations;

public class LocationsRepositoryDapper : ILocationsRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public LocationsRepositoryDapper(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<bool> CheckNameAsync(string name, CancellationToken cancellationToken)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        const string checkNameSql = """SELECT COUNT(*) FROM locations WHERE name = @Name""";
        var checkNameParams = new { Name = name };
        var count = await connection.ExecuteScalarAsync<int>(checkNameSql, checkNameParams);
        return count > 0;
    }

    public async Task<Guid> AddAsync(Location location, CancellationToken cancellationToken)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        const string locationInsertSql = """
            INSERT INTO locations (id, name, country, city, street, house_number, apartment_number, created_at, updated_at)
            VALUES (@Id, @Name, @Country, @City, @Street, @HouseNumber, @ApartmentNumber, @CreatedAt, @UpdatedAt)
        """;
        var locationInsertParams = new
        {
            Id = location.Id,
            Name = location.Name,
            Country = location.Address.Country,
            City = location.Address.City,
            Street = location.Address.Street,
            HouseNumber = location.Address.HouseNumber,
            ApartmentNumber = location.Address.ApartmentNumber,
            CreatedAt = location.CreatedAt,
            UpdatedAt = location.UpdatedAt,
        };
        await connection.ExecuteAsync(locationInsertSql, locationInsertParams);
        return location.Id;
    }
}
