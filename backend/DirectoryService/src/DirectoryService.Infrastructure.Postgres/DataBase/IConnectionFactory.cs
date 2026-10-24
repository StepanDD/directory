using System.Data;

namespace DirectoryService.Infrastructure.Postgres.DataBase;

public interface IConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}
