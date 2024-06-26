# Extending ApplicationUser
1. Create `ExtendedSecurityDbContext` class derived from `SecurityDbContext` or change the base class of your existing DbContext to `SecurityDbContext`.
Override the `OnModelCreating()` method and add `modelBuilder.UseOpenIddict();`:
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.UseOpenIddict();
        ...
    }
}
```

2. Create a temporary migration:
```powershell
dotnet ef migrations add Temporary
```

This temporary migration will contain the code that creates all the security tables.
Delete temporary migration files `*_Temporary.cs` and `*_Temporary.Designer.cs`, but keep the `ExtendedSecurityDbContextModelSnapshot.cs`.

3. Create `ExtendedApplicationUser` class derived from `ApplicationUser` and add your new fields:
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

4. In the `ExtendedSecurityDbContext.OnModelCreating()` method add your `ExtendedApplicationUser` entity:
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    
    modelBuilder.UseOpenIddict();

    modelBuilder.Entity<ExtendedApplicationUser>().Property("Discriminator").HasDefaultValue(nameof(ExtendedApplicationUser));
    ...
}
```

5. Create new migration:
```powershell
dotnet ef migrations add ExtendApplicationUser
```

6. In the `Module.Initialize()` method override `ApplicationUser` and `SecurityDbContext`:
```csharp
public void Initialize(IServiceCollection serviceCollection)
{
    ...
    AbstractTypeFactory<ApplicationUser>.OverrideType<ApplicationUser, ExtendedApplicationUser>();
    serviceCollection.AddTransient<SecurityDbContext, ExtendedSecurityDbContext>();
}
```
