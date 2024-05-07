using VirtoCommerce.Platform.Core.Security;

namespace VirtoCommerce.ExtendedSecurity.Core.Models;

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
