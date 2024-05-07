using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.ExtendedSecurity.Core.Models;
using VirtoCommerce.Platform.Security.Repositories;

namespace VirtoCommerce.ExtendedSecurity.Data.Repositories;

public class ExtendedSecurityDbContext : SecurityDbContext
{
    public ExtendedSecurityDbContext(DbContextOptions<ExtendedSecurityDbContext> options)
        : base(options)
    {
    }

    protected ExtendedSecurityDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ExtendedApplicationUser>().Property("Discriminator").HasDefaultValue(nameof(ExtendedApplicationUser));

        switch (Database.ProviderName)
        {
            case "Pomelo.EntityFrameworkCore.MySql":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.ExtendedSecurity.Data.MySql"));
                break;
            case "Npgsql.EntityFrameworkCore.PostgreSQL":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.ExtendedSecurity.Data.PostgreSql"));
                break;
            case "Microsoft.EntityFrameworkCore.SqlServer":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.ExtendedSecurity.Data.SqlServer"));
                break;
        }
    }
}
