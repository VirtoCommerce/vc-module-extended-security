using System.Reflection;
using Microsoft.EntityFrameworkCore;
//using VirtoCommerce.Platform.Data.Extensions;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace VirtoCommerce.ExtendedSecurity.Data.Repositories;

public class ExtendedSecurityDbContext : DbContextBase
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

        //modelBuilder.Entity<ExtendedSecurityEntity>().ToAuditableEntityTable("ExtendedSecurity");

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
