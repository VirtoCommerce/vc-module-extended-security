using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VirtoCommerce.ExtendedSecurity.Data.Repositories;

namespace VirtoCommerce.ExtendedSecurity.Data.SqlServer;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExtendedSecurityDbContext>
{
    public ExtendedSecurityDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ExtendedSecurityDbContext>();
        var connectionString = args.Length != 0 ? args[0] : "Server=(local);User=virto;Password=virto;Database=VirtoCommerce3;";

        builder.UseSqlServer(
            connectionString,
            options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

        return new ExtendedSecurityDbContext(builder.Options);
    }
}
