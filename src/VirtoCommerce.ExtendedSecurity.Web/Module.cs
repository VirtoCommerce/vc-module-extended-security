using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.ExtendedSecurity.Core;
using VirtoCommerce.ExtendedSecurity.Core.Models;
using VirtoCommerce.ExtendedSecurity.Data.MySql;
using VirtoCommerce.ExtendedSecurity.Data.PostgreSql;
using VirtoCommerce.ExtendedSecurity.Data.Repositories;
using VirtoCommerce.ExtendedSecurity.Data.SqlServer;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Platform.Core.Settings;
using VirtoCommerce.Platform.Security.Repositories;

namespace VirtoCommerce.ExtendedSecurity.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ExtendedSecurityDbContext>(options =>
        {
            var databaseProvider = Configuration.GetValue("DatabaseProvider", "SqlServer");
            var connectionString = Configuration.GetConnectionString(ModuleInfo.Id) ?? Configuration.GetConnectionString("VirtoCommerce");

            switch (databaseProvider)
            {
                case "MySql":
                    options.UseMySqlDatabase(connectionString);
                    break;
                case "PostgreSql":
                    options.UsePostgreSqlDatabase(connectionString);
                    break;
                default:
                    options.UseSqlServerDatabase(connectionString);
                    break;
            }
        });

        AbstractTypeFactory<ApplicationUser>.OverrideType<ApplicationUser, ExtendedApplicationUser>();
        serviceCollection.AddTransient<SecurityDbContext, ExtendedSecurityDbContext>();
    }

    public void PostInitialize(IApplicationBuilder appBuilder)
    {
        var serviceProvider = appBuilder.ApplicationServices;

        // Register settings
        var settingsRegistrar = serviceProvider.GetRequiredService<ISettingsRegistrar>();
        settingsRegistrar.RegisterSettings(ModuleConstants.Settings.AllSettings, ModuleInfo.Id);

        // Register permissions
        var permissionsRegistrar = serviceProvider.GetRequiredService<IPermissionsRegistrar>();
        permissionsRegistrar.RegisterPermissions(ModuleInfo.Id, "ExtendedSecurity", ModuleConstants.Security.Permissions.AllPermissions);

        // Apply migrations
        using var serviceScope = serviceProvider.CreateScope();
        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<ExtendedSecurityDbContext>();
        dbContext.Database.Migrate();
    }

    public void Uninstall()
    {
        // Nothing to do here
    }
}
