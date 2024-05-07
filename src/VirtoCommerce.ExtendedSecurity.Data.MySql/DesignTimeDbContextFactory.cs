using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VirtoCommerce.ExtendedSecurity.Data.Repositories;

namespace VirtoCommerce.ExtendedSecurity.Data.MySql;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExtendedSecurityDbContext>
{
    public ExtendedSecurityDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ExtendedSecurityDbContext>();
        var connectionString = args.Any() ? args[0] : "Server=localhost;User=virto;Password=virto;Database=VirtoCommerce3;";

        builder.UseMySql(
            connectionString,
            ResolveServerVersion(args, connectionString),
            options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

        return new ExtendedSecurityDbContext(builder.Options);
    }

    private static ServerVersion ResolveServerVersion(string[] args, string connectionString)
    {
        var serverVersion = args.Length >= 2 ? args[1] : null;

        if (serverVersion == "AutoDetect")
        {
            return ServerVersion.AutoDetect(connectionString);
        }

        if (serverVersion != null)
        {
            return ServerVersion.Parse(serverVersion);
        }

        return new MySqlServerVersion(new Version(5, 7));
    }
}
