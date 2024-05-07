# Extending ApplicationUser
Create `ExtendedSecurityDbContext` class derived from `SecurityDbContext` or change the base class of your existing DbContext to `SecurityDbContext`:
```csharp
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.Platform.Security.Repositories;

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
}
```

Create a temporary migration:
```powershell
dotnet ef migrations add Temporary
```

This temporary migration will contain the code that creates all the security tables.
Delete temporary migration files `*_Temporary.cs` and `*_Temporary.Designer.cs`, but keep the `ExtendedSecurityDbContextModelSnapshot.cs`.

Create `ExtendedApplicationUser` class derived from `ApplicationUser` and add your new fields:
```csharp
using VirtoCommerce.Platform.Core.Security;

public class ExtendedApplicationUser : ApplicationUser
{
    public string NewField { get; set; }

    public override void Patch(ApplicationUser target)
    {
        base.Patch(target);

        if (target is ExtendedApplicationUser extendedUser)
        {
            extendedUser.NewField = NewField;
        }
    }
}
```

In `ExtendedSecurityDbContext` override the `OnModelCreating()` method:
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<ExtendedApplicationUser>().Property("Discriminator").HasDefaultValue(nameof(ExtendedApplicationUser));
    ...
}
```

Create new migration:
```powershell
dotnet ef migrations add ExtendApplicationUser
```

In the `Module.Initialize()` method override `ApplicationUser` and `SecurityDbContext`:
```csharp
public void Initialize(IServiceCollection serviceCollection)
{
    ...
    AbstractTypeFactory<ApplicationUser>.OverrideType<ApplicationUser, ExtendedApplicationUser>();
    serviceCollection.AddTransient<SecurityDbContext, ExtendedSecurityDbContext>();
}
```
