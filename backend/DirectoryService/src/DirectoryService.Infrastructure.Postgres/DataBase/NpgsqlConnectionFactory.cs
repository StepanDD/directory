using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DirectoryService.Infrastructure.Postgres.DataBase;

public class NpgsqlConnectionFactory : IDisposable, IConnectionFactory
{
    private readonly NpgsqlDataSource _dataSource;

    public NpgsqlConnectionFactory(IConfiguration configuration)
    {
        var dataSourceBuilder = new Npgsql.NpgsqlDataSourceBuilder(configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."));
        dataSourceBuilder.UseLoggerFactory(CreateLoggerFactory());
        _dataSource = dataSourceBuilder.Build();
    }
    public async Task<IDbConnection> CreateConnectionAsync()
    {
        return await _dataSource.OpenConnectionAsync();
    }

    public void Dispose()
    {
        _dataSource.Dispose();
    }
    public async ValueTask DisposeAsync()
    {
        await _dataSource.DisposeAsync();
    }

    private ILoggerFactory CreateLoggerFactory()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });
        return loggerFactory;
    }
}
