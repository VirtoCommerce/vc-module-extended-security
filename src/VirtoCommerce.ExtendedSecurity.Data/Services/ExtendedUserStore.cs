using Microsoft.AspNetCore.Identity;
using VirtoCommerce.ExtendedSecurity.Data.Repositories;
using VirtoCommerce.Platform.Security;

namespace VirtoCommerce.ExtendedSecurity.Data.Services;

public class ExtendedUserStore(ExtendedSecurityDbContext context, IdentityErrorDescriber describer = null)
    : CustomUserStore(context, describer)
{
}
