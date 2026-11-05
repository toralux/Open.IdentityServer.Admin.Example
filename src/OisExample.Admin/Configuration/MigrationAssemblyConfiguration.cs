using System.Reflection;
using Toralux.Open.IdentityServer.Admin.EntityFramework.Configuration.Configuration;
using SqlMigrationAssembly = OisExample.Admin.EntityFramework.SqlServer.Helpers.MigrationAssembly;
using PostgreSQLMigrationAssembly = OisExample.Admin.EntityFramework.PostgreSQL.Helpers.MigrationAssembly;

namespace OisExample.Admin.Configuration;

public static class MigrationAssemblyConfiguration
{
    public static string GetMigrationAssemblyByProvider(DatabaseProviderConfiguration databaseProvider)
    {
        return (databaseProvider.ProviderType switch
        {
            DatabaseProviderType.SqlServer => typeof(SqlMigrationAssembly).GetTypeInfo().Assembly.GetName().Name,
            DatabaseProviderType.PostgreSQL => typeof(PostgreSQLMigrationAssembly).GetTypeInfo()
                .Assembly.GetName()
                .Name,
            _ => throw new ArgumentOutOfRangeException()
        })!;
    }
}
