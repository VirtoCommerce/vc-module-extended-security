using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VirtoCommerce.ExtendedSecurity.Data.Repositories;

namespace VirtoCommerce.ExtendedSecurity.Data.PostgreSql;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExtendedSecurityDbContext>
{
    public ExtendedSecurityDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ExtendedSecurityDbContext>();
        var connectionString = args.Any() ? args[0] : "Server=localhost;Username=virto;Password=virto;Database=VirtoCommerce3;";

        builder.UseNpgsql(
            connectionString,
            options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

        return new ExtendedSecurityDbContext(builder.Options);
    }
}
